using System;
using MarketMoves.Util;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketMoves.Data;
using MarketMoves.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MarketMoves.Areas.Identity;
using Microsoft.AspNetCore.Identity;

namespace MarketMoves.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AlertsManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Account> _userManager;

        public AlertsManagerController(ApplicationDbContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Alerts.ToListAsync());
        }

        // GET: Alerts1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alerts1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Entry,ProfitPrice,ProfitTarget,LossPrice,LossTarget,Title,Option,OptionType,Strike,RiskReward,Description,Image1Link,Image1Description,Image2Link,Image2Description")] Alert alert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(alert);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(alert);
        }

        // GET: Alerts1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }
            return View(alert);
        }
        
        // POST: Alerts1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Entry,ProfitPrice,ProfitTarget,LossPrice,LossTarget,Title,Option,OptionType,Strike,RiskReward,Description,Image1Link,Image1Description,Image2Link,Image2Description")] Alert alert)
        {
            if (id != alert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertExists(alert.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alert);
        }

        // GET: Alerts1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // POST: Alerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertExists(int id)
        {
            return _context.Alerts.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> ExecuteEntry(int id, double price)
        {
            var alert = await _context.Alerts.FindAsync(id);
            var message = "Zo price : " + price;
            if (price < 0)
            {
                message = "Entry price must be positive or equal to 0";
            }
            else if (alert != null)
            {
                try
                {
                    alert.LastUpdated = DateTime.Now;
                    alert.ExecutedEntry = price;
                    alert.Status = Models.Enums.AlertStatus.Executed;
                    await _context.SaveChangesAsync();
                    message = "Pozey";
                }
                catch (Exception)
                {
                    message = "Server Crash";
                }
                
            }
            JsonResult result = new JsonResult(message);


            return result;
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteExit(int id, double price)
        {
            var alert = await _context.Alerts.FindAsync(id);
            var message = "Zo price : " + price;
            if (price < 0 )
            {
                message = "Entry price must be positive or equal to 0";
            }
            else if (alert != null)
            {
                try
                {
                   
                    alert.LastUpdated = DateTime.Now;
                    alert.ExecutedExit = price;
                    alert.Status = Models.Enums.AlertStatus.Closed;
                    await _context.SaveChangesAsync();
                    message = "Pozey";
                }
                catch (Exception)
                {
                    message= "Server Crash";
                }
            }
            return Json(message);
        }
        [HttpPost]
        public async Task<IActionResult> Trigger(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            var message = "Zo " ;
            if (alert != null)
            {
                try
                {

                    alert.LastUpdated = DateTime.Now;
                    alert.Status = Models.Enums.AlertStatus.Triggered;
                    await _context.SaveChangesAsync();
                    message = "Pozey";
                }
                catch (Exception)
                {
                    message = "Server Crash";
                }
            }
            return Json(message);
        }
        [HttpPost]
        public async Task<IActionResult> Untrigger(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            var message = "Zo ";
            if (alert != null)
            {
                try
                {

                    alert.LastUpdated = DateTime.Now;
                    alert.Status = Models.Enums.AlertStatus.Untriggered;
                    await _context.SaveChangesAsync();
                    message = "Pozey";
                }
                catch (Exception)
                {
                    message = "Server Crash";
                }
            }
            return Json(message);
        }
        #region notification
        public IActionResult SendSMS(string subject, string body, bool sms, bool mail)
        {
            try
            {
                if (string.IsNullOrEmpty(body) || (!mail && !sms))
                    return Json(false);

                NotificationManager notif = new NotificationManager(_userManager);
                bool sent = false;
                if (sms)
                {
                    sent = notif.SendSMS(body);
                }
                /*if (mail)
                {
                    notif.SendMail(subject,body);
                }*/
                return Json(sent);
            }
            catch
            {
                return Json(false);
            }
        }
        #endregion
    }
}