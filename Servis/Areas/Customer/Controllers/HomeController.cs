using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Servis.Data;
using Servis.Models;
using Servis.Utility;

namespace Servis.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(User.IsInRole(SD.AdminUser))
                return RedirectToAction("UserList", "User", new { area = "Admin" });
            if (User.IsInRole(SD.TechUser))
                return RedirectToAction("ServiceList", "ServiceOrder", new { area = "Admin" });
            if (User.IsInRole(SD.CustomerUser))
                return RedirectToAction("ServiceList", "CustomerService");
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
