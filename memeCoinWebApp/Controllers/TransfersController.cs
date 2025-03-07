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
    public class TransfersController : Controller
    {
        private readonly ComContext _context;

        public TransfersController(ComContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? phoneNumber, DateTime? startDate, DateTime? endDate, int? minAmount, int? maxAmount, string? source, string? destination)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return NotFound();
            }

            var transfers = _context.Transfer
                .Where(t => t.Source == phoneNumber || t.Destination == phoneNumber)
                .AsQueryable();

            if (startDate.HasValue) transfers = transfers.Where(t => t.Timestamp >= startDate.Value);

            if (endDate.HasValue) transfers = transfers.Where(t => t.Timestamp <= endDate.Value);

            if (minAmount.HasValue) transfers = transfers.Where(t => t.Amount >= minAmount.Value);

            if (maxAmount.HasValue) transfers = transfers.Where(t => t.Amount <= maxAmount.Value);

            if (!string.IsNullOrEmpty(source)) transfers = transfers.Where(t => t.Source == source);

            if (!string.IsNullOrEmpty(destination)) transfers = transfers.Where(t => t.Destination == destination);

            var result = await transfers.ToListAsync();
            if (result == null) return NotFound();
            TempData["PhoneNumber"] = phoneNumber;
            return View(result);
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Source,Destination,Amount,Timestamp")] Transfer model)
        {
            if (model.Source == model.Destination) return RedirectToAction(nameof(Index), new { phoneNumber = model.Source});
            var srcUser = await _context.User.FirstOrDefaultAsync(u => u.PhoneNumber == model.Source);
            var dstUser = await _context.User.FirstOrDefaultAsync(u => u.PhoneNumber == model.Destination);
            if (srcUser == null || dstUser == null) return RedirectToAction(nameof(Index), new { phoneNumber = model.Source});
            if (srcUser.Balance < model.Amount) return RedirectToAction(nameof(Index), new { phoneNumber = model.Source});
            if (ModelState.IsValid)
            {
                model.Timestamp = DateTime.Now;
                _context.Add(model);
                srcUser.Balance -= model.Amount;
                dstUser.Balance += model.Amount;
                await _context.SaveChangesAsync();
            }
            TempData["PhoneNumber"] = model.Source;
            return RedirectToAction(nameof(Index), new { phoneNumber = model.Source });
        }

        private bool TransferExists(int id)
        {
            return _context.Transfer.Any(e => e.Id == id);
        }
    }
}
