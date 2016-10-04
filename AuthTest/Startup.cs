using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthTest.Startup))]
namespace AuthTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
