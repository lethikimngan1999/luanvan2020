
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(quanlybenh.App_Start.Startup))]

namespace quanlybenh.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

}
