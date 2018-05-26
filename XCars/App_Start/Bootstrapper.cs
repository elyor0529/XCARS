using Autofac;
//using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository;
using XCars.Data.Repository.Interfaces;
using XCars.Service;
//using Microsoft.AspNet.SignalR;
//using XCars.Hubs;
using XCars.Service.Interfaces;
using System.Web.Http;
using XCars.Hubs;

namespace XCars
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();

            //Configure AutoMapper
            //AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //builder.RegisterType<AuctionHub>();

            //repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<PersonalDataRepository>().As<IPersonalDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RegionRepository>().As<IRegionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoTransportTypeRepository>().As<IAutoTransportTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoBodyTypeRepository>().As<IAutoBodyTypeRepository>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoMakeRepository>().As<IAutoMakeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoModelRepository>().As<IAutoModelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyRepository>().As<ICurrencyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoRepository>().As<IAutoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoTransmissionTypeRepository>().As<IAutoTransmissionTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoDriveTypeRepository>().As<IAutoDriveTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoColorRepository>().As<IAutoColorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoFuelTypeRepository>().As<IAutoFuelTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoSecurityRepository>().As<IAutoSecurityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoComfortRepository>().As<IAutoComfortRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoMultimediaRepository>().As<IAutoMultimediaRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoStateRepository>().As<IAutoStateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoMiscRepository>().As<IAutoMiscRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoPhotoRepository>().As<IAutoPhotoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoStatusRepository>().As<IAutoStatusRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ContactsRequestRepository>().As<IContactsRequestRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionRepository>().As<IAuctionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionConnectionRepository>().As<IAuctionConnectionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SiteSettingsRepository>().As<ISiteSettingsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoExchangeRepository>().As<IAutoExchangeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoFavoriteRepository>().As<IAutoFavoriteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionPhotoRepository>().As<IAuctionPhotoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduledEmailRepository>().As<IScheduledEmailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AutoTSRegistrationRepository>().As<IAutoTSRegistrationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PurchaseTypeRepository>().As<IPurchaseTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionFavoriteRepository>().As<IAuctionFavoriteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionStatusRepository>().As<IAuctionStatusRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionBidRepository>().As<IAuctionBidRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserConnectionRepository>().As<IUserConnectionRepository>().InstancePerLifetimeScope();


            //servises
            //builder.RegisterType<SMTPEmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<SesEmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().PropertiesAutowired();
            //builder.RegisterType<PersonalDataService>().As<IPersonalDataService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<FileManager>().As<IFileManager>().InstancePerLifetimeScope();
            builder.RegisterType<RegionService>().As<IRegionService>().InstancePerLifetimeScope();
            builder.RegisterType<CityService>().As<ICityService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoTransportTypeService>().As<IAutoTransportTypeService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoBodyTypeService>().As<IAutoBodyTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoMakeService>().As<IAutoMakeService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoModelService>().As<IAutoModelService>().InstancePerLifetimeScope();
            builder.RegisterType<YearService>().As<IYearService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoService>().As<IAutoService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoTransmissionTypeService>().As<IAutoTransmissionTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoDriveTypeService>().As<IAutoDriveTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoDoorService>().As<IAutoDoorService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoColorService>().As<IAutoColorService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoFuelTypeService>().As<IAutoFuelTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoSecurityService>().As<IAutoSecurityService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoComfortService>().As<IAutoComfortService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoMultimediaService>().As<IAutoMultimediaService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoStateService>().As<IAutoStateService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoMiscService>().As<IAutoMiscService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoPhotoService>().As<IAutoPhotoService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoStatusService>().As<IAutoStatusService>().InstancePerLifetimeScope();
            builder.RegisterType<ContactsRequestService>().As<IContactsRequestService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<BillingService>().As<IBillingService>().InstancePerLifetimeScope();
            builder.RegisterType<SearchAutoService>().As<ISearchAutoService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoIndexService>().As<IAutoIndexService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionService>().As<IAuctionService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionConnectionService>().As<IAuctionConnectionService>().InstancePerLifetimeScope();
            builder.RegisterType<SiteSettingsService>().As<ISiteSettingsService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoExchangeService>().As<IAutoExchangeService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoFavoriteService>().As<IAutoFavoriteService>().InstancePerLifetimeScope();
            builder.RegisterType<AuctionPhotoService>().As<IAuctionPhotoService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoStatisticsService>().As<IAutoStatisticsService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<ScheduledEmailService>().As<IScheduledEmailService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<HangfireService>().As<IHangfireService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<HttpWebService>().As<IHttpWebService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoTSRegistrationService>().As<IAutoTSRegistrationService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AutoEngineCapacityService>().As<IAutoEngineCapacityService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoFuelConsumptionService>().As<IAutoFuelConsumptionService>().InstancePerLifetimeScope();
            builder.RegisterType<AutoPowerService>().As<IAutoPowerService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<PurchaseTypeService>().As<IPurchaseTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<HashSHA256>().As<IHash>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
            builder.RegisterType<ScriptsService>().As<IScriptsService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionFavoriteService>().As<IAuctionFavoriteService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionIndexService>().As<IAuctionIndexService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<SearchAuctionService>().As<ISearchAuctionService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionStatusService>().As<IAuctionStatusService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionStatisticsService>().As<IAuctionStatisticsService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AuctionBidService>().As<IAuctionBidService>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<UserConnectionService>().As<IUserConnectionService>().InstancePerLifetimeScope().PropertiesAutowired();



            //repository
            //builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
            //.Where(t => t.Name.EndsWith("Repository"))
            //.AsImplementedInterfaces().InstancePerLifetimeScope();

            //service
            // builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
            //.Where(t => t.Name.EndsWith("Service"))
            //.AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}