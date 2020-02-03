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
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;

namespace MarketMoves.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.Account> _userManager;

        public IEmailSender EmailSender { get; }

        public UsersController(ApplicationDbContext dbContext, UserManager<Models.Account> userManager,
            IEmailSender emailSender)
        {
            _context = dbContext;
            _userManager = userManager;
            EmailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        [AllowAnonymous]
        public async Task<ActionResult> RevokeTo(string email, string id, string password)
        {

            try
            {

                if (id != "ZoTheOptionSeller" && password != "HeIsTheBest777")
                    return Json("I did not Revoke the access; ID&PW are wrong");

                if (!_context.Users.Any(u => u.Email == email))
                    return Json("I did not Revoke the access; The email is not in the system");

                Models.Account user = await _context.Users.FirstAsync(u => u.Email == email);

                if (user == null)
                {
                    return NotFound();
                }

                user.Suscribed = false;
                await _userManager.RemoveFromRoleAsync(user, Roles.PaidUser);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User subscription revoked.";

                return Json($"I did Revoke access to {email}.");
            }
            catch (Exception)
            {
                return Json("I did not Revoke the access; Something went wrong and I crashed");
            }
        }
        public async Task<ActionResult> Revoke(string id)
        {

            try
            {

                Models.Account user = await _context.Users
                    .FirstAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Suscribed = false;
                await _userManager.RemoveFromRoleAsync(user, Roles.PaidUser);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User subscription revoked.";
            }
            catch (Exception)
            {

            }

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<ActionResult> GrantTo(string email, string id, string password)
        {

            try
            {

                if (id != "ZoTheOptionSeller" && password != "HeIsTheBest777")
                    return Json("I did not Revoke the access; ID&PW are wrong");

                if (!_context.Users.Any(u => u.Email == email))
                    return Json("I did not Revoke the access; The email is not in the system");


                Models.Account user = await _context.Users.FirstAsync(u => u.Email == email);

                if (user == null)
                {
                    return NotFound();
                }

                user.Suscribed = true;
                await _userManager.AddToRoleAsync(user, Roles.PaidUser);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User subscription granted.";

                return Json($"I did Grant access to {email}.");
            }
            catch (Exception)
            {
                return Json("I did not Revoke the access; Something went wrong and I crashed");
            }

        }
        public async Task<ActionResult> Grant(string id)
        {
            try
            {

                Models.Account user = await _context.Users.FirstAsync(u => u.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Suscribed = true;
                await _userManager.AddToRoleAsync(user, Roles.PaidUser);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User subscription granted.";
            }
            catch (Exception)
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel()
        {
            var options = new ChargeCreateOptions
            {
                Amount = 1000,
                Currency = "usd",
                Source = "tok_visa",
                ReceiptEmail = "jenny.rosen@example.com",
            };
            var service = new ChargeService();
            var charge = service.Create(options);

            return await Index();
        }
    }
}
