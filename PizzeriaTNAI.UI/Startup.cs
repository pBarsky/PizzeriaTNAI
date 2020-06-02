using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzeriaTNAI.UI.Startup))]
namespace PizzeriaTNAI.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
