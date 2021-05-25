using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    [Authorize(Roles = Domain.Constants.UserRoles.ROLE_ADMIN)]
    public class UsersController : AdminControllerBase
    {
        private IUsersControllerService _service;

        public UsersController()
        {
            _service = new UsersControllerService(new ModelStateWrapper(ModelState));
        }


        public ActionResult Index()
        {
            var vm = _service.GetIndexViewModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewRole(IndexViewModel indexViewModel)
        {
            _service.UpdateUserRole(indexViewModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            _service.DeleteUser(id);

            return RedirectToAction("Index");
        }
    }
}