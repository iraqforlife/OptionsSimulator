using System;
using System.Collections.Generic;
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

namespace MarketMoves.Controllers
{
    [Authorize(Roles = Roles.PaidUser)]
    public class AlertsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alerts.ToListAsync());
        }
        public async Task<IActionResult> History()
        {
            return View(await _context.Alerts.ToListAsync());
        }// GET: Alerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts.FirstOrDefaultAsync(m => m.Id == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }
    }
}
