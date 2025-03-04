using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using memeCoinWebApp.Data;
using memeCoinWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace memeCoinWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ComContext _context;

        public UsersController(ComContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,Password,Balance")] User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    PhoneNumber = model.PhoneNumber,
                    Password    = model.Password,
                    Balance     = 0
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData["PhoneNumber"] = model.PhoneNumber;
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login(string Message)
        {
            ViewData["Message"] = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string phonenumber, string password)
        {
            var user = _context.User
                        .FirstOrDefault(c => c.PhoneNumber == phonenumber && c.Password == password);;
            if (user != null)
            {
                TempData["PhoneNumber"] = phonenumber;
                return RedirectToAction(nameof(Index), "Home"); //Goto User Homepage
            }
            return View();

        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhoneNumber,Password,Balance")] User user)
        {
            if (id != user.PhoneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.PhoneNumber))
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
            return View(user);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.PhoneNumber == id);
        }
    }
}
