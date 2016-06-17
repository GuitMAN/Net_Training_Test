using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Net_Training_Test.Startup))]
namespace Net_Training_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
