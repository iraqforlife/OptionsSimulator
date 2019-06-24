using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketMoves.Data;
using MarketMoves.Models;

namespace MarketMoves.Controllers
{
    public class AlertsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Account> _userManager;

        public AlertsController(ApplicationDbContext dbContext, UserManager<Account> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        // GET: Performence
        public async Task<ActionResult> Index()
        {
            return View(await _dbContext.Alerts.OrderByDescending(g => g.Id).ToListAsync());
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
        // GET: Performence/Create
        public async Task<IActionResult> Show(int id)
        {
            var alert = await _dbContext.Alerts.FirstOrDefaultAsync(m => m.Id == id);

            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }
        // GET: Performence/Create
        public ActionResult AddAlert()
        {
            return View();
        }
        // POST: Performence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveAlert(IFormCollection collection)
        {
            try
            {
                String entry = collection["Entry"];
                String profitPrice = collection["ProfitPrice"];
                String profitTarget = collection["ProfitTarget"];
                String lossPrice = collection["LossPrice"];
                String lossTarget = collection["LossTarget"];
                String title = collection["Title"];
                String option = collection["Option"];
                String riskReward = collection["RiskReward"];
                String description = collection["Description"];
                String image1Link = collection["Image1Link"];
                String image1Description = collection["Image1Description"];
                String image2Link = collection["Image2Link"];
                String image2Description = collection["Image2Description"];
                DateTime created = DateTime.Now;

                Alert newAlert = new Alert()
                {
                    Created = created,
                    Description = description,
                    Entry = entry,
                    Image1Description = image1Description,
                    Image1Link = image1Link,
                    Image2Description = image2Description,
                    Image2Link = image2Link,
                    LossPrice = lossPrice,
                    LossTarget = lossTarget,
                    Option = option,
                    ProfitPrice = profitPrice,
                    ProfitTarget = profitTarget,
                    RiskReward = riskReward,
                    Status = Models.Enums.AlertStatus.OnDeck,
                    Title = title
                };
                _dbContext.Alerts.Add(newAlert);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

            return await Index();
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

                //currentUser.AddTransaction(transaction);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Performence/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var alert = await _dbContext.Alerts.FirstOrDefaultAsync(e => e.Id == id);

            if (alert == null)
            {
                return NotFound();
            }
            return View(alert);
        }

        // POST: Performence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var alert = await _dbContext.Alerts.FirstOrDefaultAsync(e => e.Id == id);

                String entry = collection["Entry"];
                String profitPrice = collection["ProfitPrice"];
                String profitTarget = collection["ProfitTarget"];
                String lossPrice = collection["LossPrice"];
                String lossTarget = collection["LossTarget"];
                String title = collection["Title"];
                String option = collection["Option"];
                String riskReward = collection["RiskReward"];
                String description = collection["Description"];
                String image1Link = collection["Image1Link"];
                String image1Description = collection["Image1Description"];
                String image2Link = collection["Image2Link"];
                String image2Description = collection["Image2Description"];

                alert.Description = description;

                alert.Entry = entry;
                alert.Image1Description = image1Description;
                alert.Image1Link = image1Link;
                alert.Image2Description = image2Description;
                alert.Image2Link = image2Link;
                alert.LossPrice = lossPrice;
                alert.LossTarget = lossTarget;
                alert.Option = option;
                alert.ProfitPrice = profitPrice;
                alert.ProfitTarget = profitTarget;
                alert.RiskReward = riskReward;
                alert.Title = title;

                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(id);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmedDelete(int id)
        {
            var alert = await _dbContext.Alerts.FirstOrDefaultAsync(e => e.Id == id);

            if (alert == null)
            {
                return NotFound();
            }

            _dbContext.Remove(alert);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // GET: Performence/Edit/5
        public async Task<ActionResult> Delete(int id)
        {
            var alert = await _dbContext.Alerts.FirstOrDefaultAsync(e => e.Id == id);

            if (alert == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}