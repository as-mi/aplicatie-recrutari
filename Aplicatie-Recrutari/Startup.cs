using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aplicatie_Recrutari.Startup))]
namespace Aplicatie_Recrutari
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
