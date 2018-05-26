using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using XCars.Hubs;
using XCars.Service;
using XCars.Data.Repository;
using XCars.Data.Infrastructure;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.Mvc;
using System.Reflection;

[assembly: OwinStartupAttribute(typeof(XCars.Startup))]
namespace XCars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //GlobalHost.DependencyResolver.Register(
            //    typeof(AuctionHub),
            //    () => new AuctionHub(
            //            new AuctionConnectionService(
            //                new AuctionConnectionRepository(new DatabaseFactory()),
            //                new UnitOfWork(new DatabaseFactory())),
            //            new UserService(
            //                new UserRepository(new DatabaseFactory()),
            //                new UnitOfWork(new DatabaseFactory())
            //                )
            //    )
            //);

            //var builder = new ContainerBuilder();
            //builder.RegisterHubs(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            //var config = new HubConfiguration();
            //var container = builder.Build();
            //config.Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            ////app.UseAutofacMiddleware(container);
            //app.MapSignalR("/signalr", config);


            //builder.RegisterFilterProvider();
            //IContainer container = builder.Build();
            //var resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            //app.MapSignalR(new HubConfiguration
            //{
            //    Resolver = resolver
            //});
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
