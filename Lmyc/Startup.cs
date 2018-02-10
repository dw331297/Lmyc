using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lmyc.Startup))]
namespace Lmyc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
