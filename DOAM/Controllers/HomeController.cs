using DOAM.Application.Services;
using DOAM.Application.Services.Interfaces;
using DOAM.Domain.Services.Elasticsearch;
using DOAM.Infrastructure.Elasticsearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAM.Controllers
{
    public class HomeController : Controller
    {
        private IHomeControllerService _service;
        private readonly IElasticClient _client;

        public HomeController()
        {
            _service = new HomeControllerService();
            _client = DOAMSearchConfiguration.GetClient();
        }

        public ActionResult Index()
        {
            var vm = _service.GetIndexViewModel(10);

            if (vm == null) return HttpNotFound();

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}