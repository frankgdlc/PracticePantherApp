using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalendarApp.Web.Startup))]
namespace CalendarApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
