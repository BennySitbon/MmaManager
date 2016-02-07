using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MmaManager.DAL;
using MmaManager.Service;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;


namespace MmaManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var container = new Container();
            //container.Register<IRepository,Repository>(Lifestyle.Singleton);
            //container.Register<IOwnershipService,OwnershipService>(Lifestyle.Transient);

            //container.Verify();

            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
