using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HalifaxDine.Startup))]
namespace HalifaxDine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
