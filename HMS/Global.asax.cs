using HMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            string tdate = DateTime.UtcNow.ToLocalTime().ToString("yyyyMMdd");
            string ttime = DateTime.UtcNow.ToLocalTime().ToLongTimeString();
            string filename = "mylog" + tdate + ".log";
            FileStream fs = new FileStream(Server.MapPath("~/log/" + filename), FileMode.Append, FileAccess.Write);
            StreamWriter swr = new StreamWriter(fs);
            string str1 = ttime + "::" + " Source: " + ex.Source;
            swr.WriteLine(str1);
            str1 = ttime + "::" + " Target: " + ex.TargetSite;
            swr.WriteLine(str1);
            str1 = ttime + "::" + " Exception: " + ex.Message;
            swr.WriteLine(str1);
            if (ex.InnerException != null)
            {
                str1 = ttime + "::" + " Inner Exception: " + ex.InnerException;
                swr.WriteLine(str1);
            }

            str1 = ttime + "::" + " Stack Trace: " + ex.StackTrace;
            swr.WriteLine(str1);
            str1 = ttime + "::" + " End of Message: ";
            swr.WriteLine(str1);
            swr.Close();

        }
    }
}
