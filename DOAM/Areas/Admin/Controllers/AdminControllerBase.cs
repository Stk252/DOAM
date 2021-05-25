using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    [Authorize(Roles = Domain.Constants.UserRoles.ROLE_ADMIN_OR_TEAM)]
    public class AdminControllerBase : Controller
    {
        
    }
}