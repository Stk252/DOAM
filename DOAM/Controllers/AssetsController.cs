using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Application.Services.Validation;
using DOAM.Application.ViewModels.Search;
using DOAM.Domain.Services;
using DOAM.Domain.Services.Elasticsearch;
using DOAM.Infrastructure.Elasticsearch;
using DOAM.Infrastructure.Elasticsearch.IndexDocuments;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Controllers
{
    public class AssetsController : Controller
    {
        private IAssetsControllerService _service;

        public AssetsController()
        {
            _service = new AssetsControllerService(new ModelStateWrapper(ModelState));
        }

        [HttpGet]
        public ActionResult Index(Domain.Models.SearchForm form)
        {
            var vm = _service.GetSearchViewModel(form);
            
            return View(vm);
        }


        public ActionResult Details(int assetId)
        {
            var vm = _service.GetDetailsViewModel(assetId);

            if (vm.Asset == null) return HttpNotFound();

            return View(vm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSuggestion()
        {
            return RedirectToAction("Index", "Assets");
        }

    }
}