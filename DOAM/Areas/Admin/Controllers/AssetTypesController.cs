using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.AssetTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    public class AssetTypesController : AdminControllerBase
    {
        private IAssetTypesControllerService _service;

        public AssetTypesController()
        {
            _service = new AssetTypesControllerService(new ModelStateWrapper(ModelState));
        }

        public AssetTypesController(IAssetTypesControllerService service)
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
            var vm = _service.GetAssetTypeFormViewModel(null);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.GetAssetTypeFormViewModel(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormViewModel vm)
        {
            if (vm.AssetType.AssetTypeId < 1)
            {
                if (!_service.CreateAssetType(vm))
                    return View("New", vm);
            }
            else
            {
                if (!_service.UpdateAssetType(vm))
                    return View("Edit", vm);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _service.DeleteAssetType(id);

            return RedirectToAction("Index");
        }
    }
}