namespace XCars.Service.Interfaces
{
    public interface IBillingService
    {
        decimal GeneratePriceForAutoPublishing(decimal oncePayedCost, int top, int days);
    }
}
