using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrackNWork.Startup))]

namespace TrackNWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
