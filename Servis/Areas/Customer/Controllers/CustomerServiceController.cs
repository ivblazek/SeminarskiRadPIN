using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servis.Data;
using Servis.Utility;

namespace Servis.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerServiceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerServiceController(ApplicationDbContext db)
        {
            _db = db;
        }
      
        public async Task<IActionResult> ServiceList()
        {
            if (!User.IsInRole(SD.CustomerUser))
                return RedirectToAction("Index");
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var order = await _db.ServiceOrder.Where(u => u.CustomerId == claim.Value).ToListAsync();
            return View(order);
        }
    }
}