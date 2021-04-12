using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(aplicatie_recrutari.Startup))]
namespace aplicatie_recrutari
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
