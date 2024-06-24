using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace S3.JWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            Console.Write($"{DateTime.Now.ToString()}");
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Console.Write($"{DateTime.Now.ToString()}");
        }
    }
}