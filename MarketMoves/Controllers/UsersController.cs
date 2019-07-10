using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MarketMoves.Areas.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketMoves.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MarketMoves.Models;

namespace MarketMoves.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;

        public UsersController(ApplicationDbContext dbContext, UserManager<Account> userManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<ActionResult> Revoke(string id)
        {
            Account user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Suscribed = false;
            await _userManager.RemoveFromRoleAsync(user, Roles.PaidUser);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "User subscription revoked.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Grant(string id)
        {
            Account user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Suscribed = true;
            await _userManager.AddToRoleAsync(user, Roles.PaidUser);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "User subscription granted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
