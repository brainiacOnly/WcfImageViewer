using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WcfImageViewer.Startup))]
namespace WcfImageViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
