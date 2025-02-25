using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTransfer.Data;
using memeCoinWebApp.Models;

namespace memeCoinWebApp.Controllers
{
    public class TransfersController : Controller
    {
        private readonly MvcTransferContext _context;

        public TransfersController(MvcTransferContext context)
        {
            _context = context;
        }

        // GET: Transfers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transfer.ToListAsync());
        }

        // GET: Transfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Source,Destination,Amount,Timestamp,UserId")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transfer);
        }

        private bool TransferExists(int id)
        {
            return _context.Transfer.Any(e => e.Id == id);
        }
    }
}
