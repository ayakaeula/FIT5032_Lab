using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyDatePicker.Startup))]
namespace MyDatePicker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
