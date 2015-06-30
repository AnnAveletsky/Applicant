using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Applicant.Startup))]
namespace Applicant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
