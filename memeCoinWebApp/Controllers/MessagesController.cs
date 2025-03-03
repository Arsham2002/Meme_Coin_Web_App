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
    public class MessagesController : Controller
    {
        private readonly ComContext _context;

        public MessagesController(ComContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chats = await _context.Message
                .Where(m => m.UserId == id)
                .GroupBy(m => m.Sender)
                .Select(g => g.OrderByDescending(m => m.Timestamp).FirstOrDefault())
                .ToListAsync();
            if (chats == null)
            {
                return NotFound();
            }
            return View(chats);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sender,Content,Seen,Timestamp,UserId")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
