using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Data;
using memeCoinWebApp.Models;

namespace memeCoinWebApp.Controllers
{
    public class FundsController : Controller
    {
        private readonly ComContext _context;

        public FundsController(ComContext context)
        {
            _context = context;
        }

        // GET: Funds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fund.ToListAsync());
        }

        // GET: Funds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fund = await _context.Fund
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fund == null)
            {
                return NotFound();
            }

            return View(fund);
        }

        // GET: Funds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Timestamp,UserId")] Fund fund)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fund);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fund);
        }

        private bool FundExists(int id)
        {
            return _context.Fund.Any(e => e.Id == id);
        }
    }
}
