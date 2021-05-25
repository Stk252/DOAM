using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.MimeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    public class MimeTypesController : AdminControllerBase
    {
        private IMimeTypesControllerService _service;

        public MimeTypesController()
        {
            _service = new MimeTypesControllerService(new ModelStateWrapper(ModelState));
        }

        public MimeTypesController(IMimeTypesControllerService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var vm = _service.GetIndexViewModel();

            return View(vm);
        }

        public ActionResult New()
        {
            var vm = _service.GetMimeTypeFormViewModel(null);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.GetMimeTypeFormViewModel(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormViewModel vm)
        {
            if (vm.MimeType.MimeTypeId < 1)
            {
                if (!_service.CreateMimeType(vm))
                    return View("New", vm);
            }
            else
            {
                if (!_service.UpdateMimeType(vm))
                    return View("Edit", vm);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _service.DeleteMimeType(id);

            return RedirectToAction("Index");
        }
    }
}