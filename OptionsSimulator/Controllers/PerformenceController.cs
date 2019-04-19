using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionsSimulator.Data;
using OptionsSimulator.Models;

namespace OptionsSimulator.Controllers
{
    public class PerformenceController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PerformenceController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        // GET: Performence
        public ActionResult Index()
        {
            return View();
        }

        // GET: Performence/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Performence/Create
        public ActionResult Transaction()
        {
            return View();
        }

        // POST: Performence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                //OptionsSimulator.Models.Account account2 = _dbContext.Accounts.Include(g => g.Transactions).Where(g => g.User.Id == currentUser.Id).FirstOrDefault();
                Transaction transaction = new Transaction();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Performence/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Performence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Performence/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Performence/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}