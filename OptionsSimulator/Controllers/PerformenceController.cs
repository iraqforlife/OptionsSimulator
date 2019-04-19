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
        private readonly UserManager<Account> _userManager;

        public PerformenceController(ApplicationDbContext dbContext, UserManager<Account> userManager)
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
        public async Task<ActionResult> AddTransaction(IFormCollection collection)
        {
            try
            {
                Account currentUser = await _userManager.GetUserAsync(User);
                DateTime open = DateTime.Now;
                DateTime close = DateTime.MinValue;

                if (!String.IsNullOrEmpty(collection["open"]))
                    open = DateTime.Parse(collection["open"]);
                if (!String.IsNullOrEmpty(collection["close"]))
                    close = DateTime.Parse(collection["close"]);
                
                double exit = 0;
                if (!String.IsNullOrEmpty(collection["exit"]))
                    exit = Double.Parse( collection["exit"]);

                double entry = 0;
                if (!String.IsNullOrEmpty(collection["entry"]))
                    entry = Double.Parse(collection["entry"]);

                DateTime expiration = DateTime.MinValue;
                if (!String.IsNullOrEmpty(collection["expiration"]))
                {
                    expiration = DateTime.Parse(collection["expiration"]);
                }
                string symbol = collection["Symbol"];
                Transaction transaction = new Transaction
                {
                    Open = open,
                    StrikePrice = Double.Parse(collection["StrikePrice"]),
                    NumberOfContracts = int.Parse(collection["NumberOfContracts"]),
                    Symbol = symbol.ToUpper(),
                    Expiration = expiration,
                    Exit = 0,
                    Gains = 0,
                    Entry = entry
                };

                if (exit > 0)
                {
                    transaction.Exit = exit;
                    transaction.Gains = (exit - entry) * transaction.NumberOfContracts;
                    if(String.IsNullOrEmpty(collection["exit"]))
                        transaction.Close = DateTime.Now;
                }

                currentUser.AddTransaction(transaction);
                await _dbContext.SaveChangesAsync();

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