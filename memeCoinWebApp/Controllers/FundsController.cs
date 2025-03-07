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

        public async Task<IActionResult> Index(string? phoneNumber, DateTime? startDate, DateTime? endDate, int? minAmount, int? maxAmount)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return NotFound();
            }

            var funds = _context.Fund
                .Where(f => f.UserPhoneNumber == phoneNumber)
                .AsQueryable();

            if (startDate.HasValue) funds = funds.Where(t => t.Timestamp >= startDate.Value);

            if (endDate.HasValue) funds = funds.Where(t => t.Timestamp <= endDate.Value);

            if (minAmount.HasValue) funds = funds.Where(t => t.Amount >= minAmount.Value);

            if (maxAmount.HasValue) funds = funds.Where(t => t.Amount <= maxAmount.Value);

            var result = await funds.ToListAsync();
            if (result == null) return NotFound();
            TempData["PhoneNumber"] = phoneNumber;
            return View(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Timestamp,UserPhoneNumber")] Fund model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.PhoneNumber == model.UserPhoneNumber);
            if (user == null) return RedirectToAction(nameof(Index), new { phoneNumber = model.UserPhoneNumber });
            if (ModelState.IsValid)
            {
                model.Timestamp = DateTime.Now;
                _context.Add(model);
                user.Balance += model.Amount;
                await _context.SaveChangesAsync();
            }
            TempData["PhoneNumber"] = model.UserPhoneNumber;
            return RedirectToAction(nameof(Index), new { phoneNumber = model.UserPhoneNumber });
        }
    }
}
