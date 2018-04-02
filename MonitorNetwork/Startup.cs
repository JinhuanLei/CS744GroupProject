using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MonitorNetwork.Startup))]
namespace MonitorNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
