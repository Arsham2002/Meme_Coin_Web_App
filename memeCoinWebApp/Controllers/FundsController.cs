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
        public async Task<IActionResult> Index(string? phoneNumber) 
        {
            return View(await _context.Fund.ToListAsync());
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
            ViewData.Timestamp = DateTime.Now;

            return View(find_user);
        }

    }
}
