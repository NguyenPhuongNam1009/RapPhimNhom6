using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nhom6_DoAn_.Startup))]
namespace Nhom6_DoAn_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
