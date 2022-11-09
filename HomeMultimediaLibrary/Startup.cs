using HomeMultimediaLibrary.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeMultimediaLibrary.Startup))]
namespace HomeMultimediaLibrary
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);         
        }
    }
}
