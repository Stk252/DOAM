using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    public class AdminController : AdminControllerBase
    {
        // Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}