using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using PersonApplication.AutofacWeb;

namespace PersonApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Starting the application...");
            
          
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            if (exception != null)
            {
                log.Error("Application Unhandled Error in Global Exception", Server.GetLastError());
                Server.ClearError();
                Response.Redirect("Error");
            }

        }

       
    }
}
