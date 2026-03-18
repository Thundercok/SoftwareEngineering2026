using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerWForms.Startup))]
namespace CustomerWForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
