using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketMoves.Models;
using MarketMoves.Data;
using Microsoft.EntityFrameworkCore;

namespace MarketMoves.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext Context { get; }

        public HomeController(ApplicationDbContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> History()=> View(await Context.Alerts.ToListAsync());

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
