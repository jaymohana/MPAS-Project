using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MPAS.WebUI.Startup))]
namespace MPAS.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
