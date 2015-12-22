using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MmaManager.Startup))]
namespace MmaManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
