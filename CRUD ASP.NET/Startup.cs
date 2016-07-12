using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUD_ASP.NET.Startup))]
namespace CRUD_ASP.NET
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
