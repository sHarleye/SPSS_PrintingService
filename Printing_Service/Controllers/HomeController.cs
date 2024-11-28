using Microsoft.AspNetCore.Mvc;
using Printing_Service.Models;
using System.Diagnostics;

namespace Printing_Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly string filename = "sample.pdf";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LoginSPSO()
        {
            return View();
        }

    
        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult MainPageSPSO()
        {
            return View();
        }

        public IActionResult EditSystemSPSO()
        {
            return View();
        }

        public IActionResult ConfigUpload()
        {
            return View();
        }

        public IActionResult ConfirmConfig()
        {
            ViewBag.Filename = filename;
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult UploadConfig()
        {
            return View();
        }

        public IActionResult UploadConfirm()
        {
            return View();
        }

        public IActionResult BuyPaper()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
