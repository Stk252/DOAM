using System;
using System.Collections.Generic;
using System.Linq;
using DOAM.Application.Services;
using System.Web;
using DOAM.Application.ViewModels.MetadataGroups;
using System.Web.Mvc;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;

namespace DOAM.Areas.Admin.Controllers
{
    public class MetadataGroupsController : AdminControllerBase
    {
        private IMetadataGroupsControllerService _service;

        public MetadataGroupsController()
        {
            _service = new MetadataGroupsControllerService(new ModelStateWrapper(ModelState));
        }

        public MetadataGroupsController(IMetadataGroupsControllerService service)
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
            var vm = _service.GetMetadataGroupFormViewModel(null);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.GetMetadataGroupFormViewModel(id);

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormViewModel vm)
        {
            if (vm.MetadataGroup.MetadataGroupId < 1)
            {
                if (!_service.CreateMetadataGroup(vm))
                {
                    return View("New", vm);
                }
            } 
            else
            {
                if (!_service.UpdateMetadataGroup(vm))
                    return View("Edit", vm);
            }



            return RedirectToAction("Index");       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) 
        {
            _service.DeleteMetadataGroup(id);

            return RedirectToAction("Index");
        }
    }
}