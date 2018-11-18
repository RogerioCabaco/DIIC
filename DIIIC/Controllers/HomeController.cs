using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIIIC.Models;

namespace DIIIC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            RequestsViewModel viewModel = new RequestsViewModel();

            return View(viewModel);
        }

        public IActionResult RenovationOrders()
        {
            ViewData["Message"] = "Your RenovationOrders page.";

            return View();
        }

        public IActionResult HistorialOrders()
        {
            ViewData["Message"] = "Your HistorialOrders page.";

            return View();
        }

        public IActionResult Privacy()
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
