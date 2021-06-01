using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SEP21.Startup))]
namespace SEP21
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
