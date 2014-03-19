using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstateApp.Startup))]
namespace RealEstateApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
