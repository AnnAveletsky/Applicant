using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApplicantWeb.Startup))]
namespace ApplicantWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
