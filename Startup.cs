using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskMA.Startup))]
namespace TaskMA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
