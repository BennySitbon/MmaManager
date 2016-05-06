using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MmaManager
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            //var container = new Container();
            //container.Register<IRepository,Repository>(Lifestyle.Singleton);
            //container.Register<IOwnershipService,OwnershipService>(Lifestyle.Transient);

            //container.Verify();

            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
