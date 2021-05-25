using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    public class AssetsController : AdminControllerBase
    {
        private IAssetsControllerService _service;

        public AssetsController()
        {
            _service = new AssetsControllerService(new ModelStateWrapper(ModelState));
        }


        public ActionResult Index()
        {
            var vm = _service.GetIndexViewModel();

            return View(vm);
        }


        public ActionResult New()
        {
            var vm = _service.GetAssetFormViewModel(null);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.GetAssetFormViewModel(id);

            return View(vm);
        }

        public ActionResult AssetMetadatas(int? assetId, int mimeTypeId) {
            
            var vm = _service.GetAssetMetadatasViewModel(assetId, mimeTypeId);

            return PartialView("_AssetMetadatas", vm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormViewModel formViewModel, AssetMetadatasViewModel assetMetadatasViewModel)
        {
            if (formViewModel.Asset.AssetId < 1)
            {
                if (!_service.CreateAsset(formViewModel, assetMetadatasViewModel))
                    return View("New", formViewModel);

            }
            else
            {
                if (!_service.UpdateAsset(formViewModel, assetMetadatasViewModel))
                    return View("Edit", formViewModel);
            }


            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _service.DeleteAsset(id);

            return RedirectToAction("Index");
        }



        public ActionResult Issues()
        {
            return View();
        }

        public ActionResult Suggestions()
        {
            return View();
        }


    }
}