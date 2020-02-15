using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servis.Data;
using Servis.Models;
using Servis.Utility;

namespace Servis.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> UserList()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(await _db.AppUser.Where(u => u.Id != claim.Value).OrderBy(u=> u.Name).ToListAsync());
        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _db.AppUser.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var user = await _db.AppUser.FindAsync(id);
            _db.AppUser.Remove(user);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(UserList));
        }


        public async Task<IActionResult> LockUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appUser = await _db.AppUser.FirstOrDefaultAsync(i => i.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            appUser.LockoutEnd = DateTime.Now.AddYears(100);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(UserList));
        }

        public async Task<IActionResult> UnlockUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appUser = await _db.AppUser.FirstOrDefaultAsync(i => i.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            appUser.LockoutEnd = DateTime.Now;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(UserList));
        }
    }
}