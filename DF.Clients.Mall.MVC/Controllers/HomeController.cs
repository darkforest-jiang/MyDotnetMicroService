using DF.Clients.Mall.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DF.Clients.Mall.MVC.Helper;

namespace DF.Clients.Mall.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceHelper _serviceHelper;

        public HomeController(ILogger<HomeController> logger, IServiceHelper serviceHelper)
        {
            _logger = logger;
            _serviceHelper = serviceHelper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.GoodsData = await _serviceHelper.GetGoods();
            ViewBag.OrderData = await _serviceHelper.GetOrder();

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
