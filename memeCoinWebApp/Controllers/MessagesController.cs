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
        public async Task<IActionResult> Index(string? phoneNumber)
        {
            if (phoneNumber == null)
            {
                return NotFound();
            }

            var chats = await _context.Message
                .Where(m => m.UserPhoneNumber == phoneNumber)
                .GroupBy(m => m.Sender)
                .Select(g => g.OrderByDescending(m => m.Timestamp).FirstOrDefault())
                .ToListAsync();
            TempData["PhoneNumber"] = phoneNumber;
            return View(chats);
        }

        public async Task<IActionResult> Details(string? phoneNumber, string? senderPhoneNumber)
        {
            if (phoneNumber == null || senderPhoneNumber == null)
            {
                return NotFound();
            }
            var messages = await _context.Message
                .Where(m => (m.UserPhoneNumber == phoneNumber && m.Sender == senderPhoneNumber)
                            || (m.UserPhoneNumber == senderPhoneNumber && m.Sender == phoneNumber))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
            ViewData["Sender"] = senderPhoneNumber;
            TempData["PhoneNumber"] = phoneNumber;
            var notSeen = await _context.Message
                .Where(m => m.UserPhoneNumber == phoneNumber && m.Sender == senderPhoneNumber && m.Seen == false)
                .ToListAsync();
            foreach (var message in notSeen)
            {
                message.Seen = true;
            }
            await _context.SaveChangesAsync();
            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string content, string sender, string recipient)
        {
            Message newMessage = new Message
            {
                Sender = sender,
                Content = content,
                Seen = false,
                Timestamp = DateTime.Now,
                UserPhoneNumber = recipient
            };
            _context.Add(newMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { phoneNumber = sender, senderPhoneNumber = recipient });
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
