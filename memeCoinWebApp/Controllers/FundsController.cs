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

        public async Task<IActionResult> IncreaseAmount(string? phoneNumber, int value)
        {
            if (phoneNumber == null)
            {
                Fund f1 = new Fund();
                User u1 = new User();
                u1.PhoneNumber = "2";
                u1.Password = "1";
                u1.Balance = 2;

                f1.Id = 1;
                f1.Amount = 2139;
                f1.Timestamp = DateTime.Now;
                f1.UserPhoneNumber = "2";

            }

            var find_user = await _context.Fund
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.User.PhoneNumber == phoneNumber);

            if (find_user == null)
            {
                return NotFound();
            }

            var last_value = find_user.Amount;
            ViewData["Amount"] = last_value + value;
            // ViewData.Timestamp = DateTime.Now;

            return View(find_user);
        }

        [HttpPost]
        public async Task<IActionResult> TransferMoney(string? SrcPhoneNumber, string? DestPhoneNumber, int value)
        {
            if (SrcPhoneNumber == null || DestPhoneNumber == null)
            {
                return NotFound("حساب‌ها نامعتبر است");
            }
    
            var find_src = await _context.Fund
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.User.PhoneNumber == SrcPhoneNumber);

            
            var find_dest = await _context.Fund
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.User.PhoneNumber == DestPhoneNumber);

            if (find_src.Amount < value)
            {
                return NotFound("موجودی کافی نیست");
            }

            find_src.Amount -= value;
            find_dest.Amount += value;

            return View(find_src);
        }

        public async Task<IActionResult> TransactionList(string? phoneNumber, DateTime? startDate, DateTime? endDate, int? startFee, int? endFee, string? senderName, string? receiverName)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return NotFound("شماره تلفن وارد نشده است");
            }

            var transactions = _context.Transfer
                .Where(t => t.Source == phoneNumber || t.Destination == phoneNumber)
                .AsQueryable();

            if (startDate.HasValue)
                transactions = transactions.Where(t => t.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                transactions = transactions.Where(t => t.Timestamp <= endDate.Value);

            if (startFee.HasValue)
                transactions = transactions.Where(t => t.Amount >= startFee.Value);

            if (endFee.HasValue)
                transactions = transactions.Where(t => t.Amount <= endFee.Value);

            if (!string.IsNullOrEmpty(senderName))
            {
                var senderUsers = _context.User
                    .Where(u => u.Password.Contains(senderName))
                    .Select(u => u.PhoneNumber)
                    .ToList();

                transactions = transactions.Where(t => senderUsers.Contains(t.Source));
            }

            if (!string.IsNullOrEmpty(receiverName))
            {
                var receiverUsers = _context.User
                    .Where(u => u.Password.Contains(receiverName))
                    .Select(u => u.PhoneNumber)
                    .ToList();

                transactions = transactions.Where(t => receiverUsers.Contains(t.Destination));
            }

            var result = await transactions.ToListAsync();
            return View(result);
        }
    }
}
