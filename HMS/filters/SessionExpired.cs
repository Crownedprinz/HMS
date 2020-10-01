using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using anchor1.utilities;
using System.Web.Routing;

namespace anchor1.Filters
{
    public class SessionExpired : ActionFilterAttribute, IActionFilter
    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check if session is supported
            if (ctx.Session != null)
            {

                // check if a new session id was generated
                if (ctx.Session.IsNewSession)
                {

                    // If it says it is a new session, but an existing cookie exists, then it must
                    // have timed out
                    string sessionCookie = ctx.Request.Headers["Cookie"];
                    if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(new
                            {
                                controller = "Home",
                                action = "signout"
                            }));

                        //var url = new UrlHelper(filterContext.RequestContext);
                        //var loginUrl = url.Content("~/home/signout");
                        //filterContext.HttpContext.Response.Redirect(loginUrl, true);
                        ////ctx.Response.Redirect("~/home/signout");
                    }
                }
            }

            this.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptionActionAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
           Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            if (HttpContext.Current.Request.QueryString.Get("anc") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("anc");
                string decrptedString = Ccheckg.convert_pass2(encryptedQueryString.ToString(),1);
                string[] paramsArrs = decrptedString.Split('?');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    decryptedParameters.Add(paramArr[0], (paramArr[1]));
                }
            }
            for (int i = 0; i < decryptedParameters.Count; i++)
            {
                filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }

            this.OnActionExecuting(filterContext);
        }

    
    }



}