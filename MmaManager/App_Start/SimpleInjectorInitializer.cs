using Domain.DAL;
using Service.Entity;

[assembly: WebActivator.PostApplicationStartMethod(typeof(MmaManager.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace MmaManager.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
//#error Register your services here (remove this line).

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.Register<IRepository, Repository>(Lifestyle.Singleton);
            container.Register<IOwnershipService, OwnershipService>(Lifestyle.Transient);
            container.Register<IMarketplaceService,MarketplaceService>(Lifestyle.Transient);
            container.Register<IUserStatisticsService,UserStatisticsService>(Lifestyle.Transient);
        }
    }
}