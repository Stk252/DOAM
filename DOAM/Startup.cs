using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOAM.Startup))]
namespace DOAM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
