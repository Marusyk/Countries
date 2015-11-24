using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Countries
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_Error(object sender, EventArgs e)
        {            
            var exc = Server.GetLastError();

            Response.Write("<h2>Error Page </h2>\n");
            Response.Write(
                "<p>" + exc.Message + "</p>\n");
            Server.ClearError();
        }
    }
}
