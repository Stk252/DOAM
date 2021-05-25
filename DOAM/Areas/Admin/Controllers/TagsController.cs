using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Areas.Admin.Controllers
{
    public class TagsController : AdminControllerBase
    {
        private ITagsControllerService _service;

        public TagsController()
        {
            _service = new TagsControllerService(new ModelStateWrapper(ModelState));
        }

        public TagsController(ITagsControllerService service)
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
            var vm = _service.GetTagFormViewModel(null);

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var vm = _service.GetTagFormViewModel(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FormViewModel vm)
        {
            if (vm.Tag.TagId < 1)
            {
                if (!_service.CreateTag(vm))
                    return View("New", vm);
            }
            else
            {
                if (!_service.UpdateTag(vm))
                    return View("Edit", vm);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _service.DeleteTag(id);

            return RedirectToAction("Index");
        }
    }
}