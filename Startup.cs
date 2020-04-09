using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HumberAreaHospitalProject.Startup))]
namespace HumberAreaHospitalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
