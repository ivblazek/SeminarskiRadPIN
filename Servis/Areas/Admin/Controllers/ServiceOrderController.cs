using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servis.Data;
using Servis.Models;
using Servis.Models.ViewModels;
using Servis.Utility;
using static Servis.Models.ViewModels.ServiceOrderVM;

namespace Servis.Areas.Admin.Controllers
{
    [Authorize(Roles=SD.AdminUser + "," + SD.TechUser)]
    [Area("Admin")]
    public class ServiceOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServiceOrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> NewServiceOrder()
        {
            
            ServiceOrderVM order = new ServiceOrderVM()
            {
                
                CustomerList = await _db.AppUser.OrderBy(i => i.Name).Where(i => i.Customer == 1).ToListAsync(),
                ServiceOrder = new Models.ServiceOrder()
            };

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewServiceOrder(ServiceOrderVM order)
        {
            
            order.ServiceOrder.DateReceived = DateTime.Now;
            order.ServiceOrder.DateLastChange = DateTime.Now;
            order.ServiceOrder.Status = 0;

            if (ModelState.IsValid)
            {
                _db.ServiceOrder.Add(order.ServiceOrder);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ServiceList));
            }

            ServiceOrderVM orderVM = new ServiceOrderVM()
            {
                CustomerList = await _db.AppUser.OrderBy(i => i.Name).Where(i => i.Customer == 1).ToListAsync(),
                ServiceOrder = new Models.ServiceOrder()
            };
            return View(orderVM);
        }
        

        public async Task<IActionResult> ServiceList()
        {
            var order = await _db.ServiceOrder.ToListAsync();
            return View(order);
        }
                

        public async Task<IActionResult> EditServiceOrder(int? id)
        {
            List<Status> status = new List<Status>();
            status.Add(new Status { Id = 0, Name = "U obradi" });
            status.Add(new Status { Id = 1, Name = "Čeka dijelove" });
            status.Add(new Status { Id = 2, Name = "Čeka preuzimanje" });
            status.Add(new Status { Id = 3, Name = "Preuzet" });

            if (id == null) 
            {
                return NotFound();
            }
            var order = await _db.ServiceOrder.FindAsync(id);
            if(order==null)
            {
                return NotFound();
            }

            ServiceOrderVM orderVM = new ServiceOrderVM()
            {
                CustomerList = await _db.AppUser.OrderBy(i => i.Name).Where(i => i.Customer == 1).ToListAsync(),
                StatusList = status,
                ServiceOrder = order
            };

            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditServiceOrder(ServiceOrderVM order)
        {
            order.ServiceOrder.DateLastChange = DateTime.Now;
            if (ModelState.IsValid)
            {
                _db.ServiceOrder.Update(order.ServiceOrder);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ServiceList));
            }
            return View(order);
        }


        public async Task<IActionResult> ServiceDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _db.ServiceOrder.Include(i=>i.Customer).SingleOrDefaultAsync(i=> i.Id==id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _db.ServiceOrder.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var order = await _db.ServiceOrder.FindAsync(id);
            _db.ServiceOrder.Remove(order);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ServiceList));
        }

    }
}