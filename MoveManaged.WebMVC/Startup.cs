using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoveManaged.WebMVC.Startup))]
namespace MoveManaged.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
