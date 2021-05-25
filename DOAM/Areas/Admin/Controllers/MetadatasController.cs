using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Metadatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    public class MetadatasController : AdminControllerBase
    {
        private IMetadatasControllerService _service;

        public MetadatasController()
        {
            _service = new MetadatasControllerService(new ModelStateWrapper(ModelState));
        }

        public MetadatasController(IMetadatasControllerService service)
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
            var vm = _service.GetMetadataFormViewModel(null);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.GetMetadataFormViewModel(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormViewModel vm)
        {
            if (vm.Metadata.MetadataId < 1)
            {
                if (!_service.CreateMetadata(vm))
                    return View("New", vm);
            }
            else
            {
                if (!_service.UpdateMetadata(vm))
                    return View("Edit", vm);
            }



            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _service.DeleteMetadata(id);

            return RedirectToAction("Index");
        }
    }
}