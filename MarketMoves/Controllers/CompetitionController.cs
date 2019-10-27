using MarketMoves.Areas.Identity;
using MarketMoves.Data;
using MarketMoves.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MarketMoves.Controllers
{
    [Authorize(Roles = Roles.PaidUser)]
    public class CompetitionController : Controller
    {
        private ApplicationDbContext Context;
        private readonly UserManager<Account> UserManager;


        public CompetitionController(ApplicationDbContext context, UserManager<Account> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        // GET: Competition
        public ActionResult Leader()
        {
            var users = Context.Accounts.Include(o => o.Plays).Where(p => p.Plays.Count > 0 && p.Plays.Any(q => q.Locked) && p.Balance>0);

            return View(users);
        }
        // GET: Competition/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Play play = await Context.Plays.FirstAsync(p => p.Id == id);
            return View(play);
        }
        // GET: Competition/Create
        public ActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> MyTrades()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }
            Account account = Context.Accounts.Include(x => x.Plays).First(u => u.Id == user.Id);
            return View(account);
        }
        public async Task<IActionResult> UserTrades(string id)
        {
            Account user = await Context.Accounts.Include(o=>o.Plays).FirstAsync(a=>a.Id==id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }
            return View(user);
        }
        // POST: Competition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var user = await UserManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
                    }
                    double entry = 0;
                    double profit = 0;
                    if (!string.IsNullOrEmpty(collection["Entry"]))
                    {
                        entry = double.Parse(collection["Entry"]);
                        profit -= entry;
                    }
                    double exit = 0;
                    if (!string.IsNullOrEmpty(collection["Exit"]))
                    {
                        exit = double.Parse(collection["Exit"]);
                        profit += exit;
                    }
                    string locked = collection["Locked"];
                    locked = locked.Split(',')[0];
                    Play play = new Play()
                    {
                        Entry = entry,
                        Exit = exit,
                        Profit = profit,
                        Why = collection["Why"],
                        Learning = collection["Learning"],
                        AccountId = user.Id,
                        Execution = collection["Execution"],
                        Locked = bool.Parse(locked),
                        Proof = collection["Proof"],
                        Title = collection["Title"]
                    };

                    Account account = await Context.Accounts.Include(o=>o.Plays).FirstAsync(a => a.Id == user.Id);
                    if (play.Locked)
                    {
                        if (account.Balance == 0 && account.Plays.Count == 0)
                        {
                            account.Balance = 500;
                        }

                        account.Balance += play.Profit;
                    }
                    account.Plays.Add(play);
                    await Context.SaveChangesAsync();

                    return RedirectToAction(nameof(MyTrades));
                }
                return Create();
            }
            catch
            {
                return RedirectToAction(nameof(MyTrades));
            }
        }
        // GET: Competition/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Play play = await Context.Plays.FirstAsync(p => p.Id == id);
                return View(play);
                
            }
            catch (System.Exception)
            {
                return RedirectToAction(nameof(MyTrades));
            }

        }
        // POST: Competition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var user = await UserManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
                    }
                    double entry = 0;
                    double profit = 0;
                    if (!string.IsNullOrEmpty(collection["Entry"]))
                    {
                        entry = double.Parse(collection["Entry"]);
                        profit -= entry;
                    }
                    double exit = 0;
                    if (!string.IsNullOrEmpty(collection["Exit"]))
                    {
                        exit = double.Parse(collection["Exit"]);
                        profit += exit;
                    }
                    string locked = collection["Locked"];
                    locked = locked.Split(',')[0];
                    Play play = new Play()
                    {
                        Entry = entry,
                        Exit = exit,
                        Profit = profit,
                        Why = collection["Why"],
                        Learning = collection["Learning"],
                        AccountId = user.Id,
                        Execution = collection["Execution"],
                        Locked = bool.Parse(locked),
                        Proof = collection["Proof"],
                        Title = collection["Title"]
                    };

                    Account account = await Context.Accounts.Include(o=>o.Plays).FirstAsync(a => a.Id == user.Id);
                    if (play.Locked)
                    {
                        if(account.Balance == 0 && account.Plays.Count == 0)
                        {
                            account.Balance = 500;
                        }

                        account.Balance += play.Profit;
                    }
                    account.Plays.First(p => p.Id == id).Update(play);
                    await Context.SaveChangesAsync();

                    return RedirectToAction(nameof(MyTrades));
                }
                return await Edit(id);
            }
            catch
            {
                return View();
            }
        }
    }
}