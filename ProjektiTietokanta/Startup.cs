using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektiTietokanta.Startup))]
namespace ProjektiTietokanta {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
