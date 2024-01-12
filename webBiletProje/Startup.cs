using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webBiletProje.Startup))]
namespace webBiletProje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
