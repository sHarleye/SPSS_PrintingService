using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
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

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ProfileSPSO()
        {
            return View();
        }

        public IActionResult EditPrinter()
        {
            return View();
        }

        public IActionResult StudentHistory(string sortOrder, string searchString)
        {
            // Simulate getting values print1-5 from database
            var print1 = new PrintHistory()
            {
                Date = new DateTime(2024, 12, 3, 8, 0, 0),
                FileName = "SampleFile.pdf",
                PageA4 = "A4",
                Printer = 1,
                Side = "2 mặt",
                Scale = "Toàn bộ",
                Orientation = "Dọc"

            };

            var print2 = new PrintHistory()
            {
                Date = new DateTime(2024, 11, 30, 8, 0, 0),
                FileName = "SampleFile.pdf",
                PageA4 = "A4",
                Printer = 1,
                Side = "2 mặt",
                Scale = "Toàn bộ",
                Orientation = "Dọc"
            };

            var print3 = new PrintHistory()
            {
                Date = new DateTime(2024, 12, 2, 10, 0, 0),
                FileName = "SampleFile.pdf",
                PageA4 = "A4",
                Printer = 1,
                Side = "2 mặt",
                Scale = "Toàn bộ",
                Orientation = "Dọc"
            };

            var print4 = new PrintHistory()
            {
                Date = new DateTime(2024, 11, 27, 8, 0, 0),
                FileName = "SampleFile.pdf",
                PageA4 = "A4",
                Printer = 1,
                Side = "2 mặt",
                Scale = "Toàn bộ",
                Orientation = "Dọc"
            };

            var print5 = new PrintHistory()
            {
                Date = new DateTime(2024, 11, 30, 7, 0, 0),
                FileName = "SampleFile.pdf",
                PageA4 = "A4",
                Printer = 1,
                Side = "2 mặt",
                Scale = "Toàn bộ",
                Orientation = "Dọc"
            };

            var printL = new List<PrintHistory>();
            printL.Add(print1);
            printL.Add(print2); 
            printL.Add(print3);
            printL.Add(print4);
            printL.Add(print5);

            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";
            ViewData["CurrentFilter"] = searchString;
            
            var print = from s in printL select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (DateTime.TryParse(searchString, out var dateTime))
                {
                    print = print.Where(s => s.Date.ToShortDateString() == dateTime.ToShortDateString());
                }
            }

            switch (sortOrder)
            {
                case "dateDesc":
                    print = print.OrderByDescending(s => s.Date);
                    break;
                default:
                    print = print.OrderBy(s => s.Date);
                    break;
            }

            return View(print.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
