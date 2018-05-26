using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public IAutoService AutoService { get; set; }
        public IHangfireService HangfireService { get; set; }
        public IHash Hash { get; set; }
        public ITransactionService TransactionService { get; set; }

        public OrderService(IOrderRepository orderRepository,
                            IUnitOfWork unitOfWork)
            : base(orderRepository, unitOfWork)
        {
        }

        public Order GetByGUID(string guid)
        {
            return _repository.Get(o => o.LMI_PAYMENT_NO == guid);
        }

        public void Create(Order model)
        {
            model.LMI_PAYMENT_NO = Guid.NewGuid().ToString();
            this._repository.Add(model);
            Save();
        }

        public void Edit(Order model)
        {
            this._repository.Update(model);
            Save();
        }

        public void Delete(Order model)
        {
            this._repository.Delete(model);
            Save();
        }

        public bool IsValid(dynamic orderVM)
        {
            try
            {
                //Order order = GetByID(orderVM.LMI_PAYMENT_NO);
                Order order = GetByGUID(orderVM.LMI_PAYMENT_NO);
                if (order == null)
                    return false;

                int merchantID = 0;
                int.TryParse(XCarsConfiguration.LMI_MERCHANT_ID, out merchantID);

                double LMI_PAYMENT_AMOUNT = 0;
                double.TryParse(GetDynamicProperty(orderVM, "LMI_PAYMENT_AMOUNT"), out LMI_PAYMENT_AMOUNT);

                if (orderVM.LMI_MERCHANT_ID != merchantID
                    || !order.IsOpen
                    || orderVM.LMI_PAYMENT_AMOUNT != (double)order.LMI_PAYMENT_AMOUNT)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Process(dynamic orderVM)
        {
            try
            {
                if (orderVM.LMI_HASH != Hash.GetHash(orderVM.LMI_MERCHANT_ID + orderVM.LMI_PAYMENT_NO + orderVM.LMI_PAYMENT_AMOUNT.ToString().Replace(',', '.') + XCarsConfiguration.LMI_secretKey))
                    return;

                string guid = GetDynamicProperty(orderVM, "LMI_PAYMENT_NO");
                Order order = GetByGUID(guid);
                if (order == null)
                    return;

                order.IsOpen = false;
                order.User.Balance -= order.UsedFromBalance;

                //оплата за публикацию авто
                if (order.PurchaseTypeID == 1)
                {
                    int top = 0;
                    OrderDetail detailTop = order.OrderDetails.FirstOrDefault(d => d.Name == "top");
                    if (detailTop != null)
                        int.TryParse(detailTop.Value, out top);

                    int days = 0;
                    OrderDetail detailDays = order.OrderDetails.FirstOrDefault(d => d.Name == "days");
                    if (detailDays != null)
                        int.TryParse(detailDays.Value, out days);

                    try
                    {
                        int autoID = 0;
                        if (order.ObjectID != null)
                            autoID = (int)order.ObjectID;
                        Auto auto = AutoService.GetByID(autoID);
                        if (auto == null)
                            return;

                        DateTime dateExpires = DateTime.Now.AddDays(days);
                        AutoService.Publish(auto, dateExpires);
                        auto.CompletionJobID = HangfireService.CreateJobForAuto(auto);
                        auto.Top = top;
                        AutoService.Edit(auto);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (order.PurchaseTypeID == 2)
                {
                    //........
                }

                Edit(order);

                int LMI_SYS_PAYMENT_ID = 0;
                int.TryParse(GetDynamicProperty(orderVM, "LMI_SYS_PAYMENT_ID"), out LMI_SYS_PAYMENT_ID);

                //create transaction
                Transaction transaction = new Transaction()
                {
                    Amount = order.LMI_PAYMENT_AMOUNT,
                    DateCreated = DateTime.Now,
                    ObjectID = order.ObjectID,
                    PurchaseTypeID = order.PurchaseTypeID,
                    StateID = 1,
                    UserID = order.UserID,
                    LMI_PAYMENT_SYSTEM = GetDynamicProperty(orderVM, "LMI_PAYMENT_SYSTEM"),
                    LMI_SYS_PAYMENT_ID = LMI_SYS_PAYMENT_ID,
                    LMI_PAYER_IDENTIFIER = GetDynamicProperty(orderVM, "LMI_PAYER_IDENTIFIER") ?? "unknown",
                    LMI_MODE = order.LMI_MODE
                };

                DateTime dtTmp;
                if (DateTime.TryParse(GetDynamicProperty(orderVM, "LMI_SYS_PAYMENT_DATE"), out dtTmp))
                //if (DateTime.TryParse("2018-01-16 20:56:00", out dtTmp))
                    transaction.LMI_SYS_PAYMENT_DATE = dtTmp;

                TransactionService.Create(transaction);
            }
            catch (Exception ex)
            {
            }
        }

        private string GetDynamicProperty(dynamic obj, string nameOfProperty)
        {
            try
            {
                var propertyInfo = obj.GetType().GetProperty(nameOfProperty);
                string value = propertyInfo.GetValue(obj, null);

                return value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}