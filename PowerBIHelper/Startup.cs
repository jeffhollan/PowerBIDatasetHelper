using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerBIHelper.Startup))]
namespace PowerBIHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
