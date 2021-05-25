using DOAM.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Helpers
{
    public static class Utils
    {
        public static string IsLinkActive(this UrlHelper url, string action, string controller)
        {
            if (url.RequestContext.RouteData.Values["controller"].ToString() == controller &&
                url.RequestContext.RouteData.Values["action"].ToString() == action)
            {
                return "active";
            }

            return "";
        }

        public static string IsMenuActive(this UrlHelper url, string controller)
        {
            if (url.RequestContext.RouteData.Values["controller"].ToString() == controller)
            {
                return "active";
            }

            return "";
        }


        public static string IsMenuOpen(this UrlHelper url, string controller)
        {
            if (url.RequestContext.RouteData.Values["controller"].ToString() == controller)
            {
                return "menu-open";
            }

            return "";
        }
    }
}