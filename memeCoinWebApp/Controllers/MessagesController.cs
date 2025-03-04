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
                Message m1 = new Message();
                m1.Id = 1;
                m1.Sender = "1";
                m1.Content = "Hello!";
                m1.Seen = true;
                m1.Timestamp = DateTime.Now;
                m1.UserPhoneNumber = "2";
                Message m2 = new Message();
                m2.Id = 2;
                m2.Sender = "2";
                m2.Content = "Hello back!";
                m2.Seen = true;
                m2.Timestamp = DateTime.Now;
                m2.UserPhoneNumber = "1";
                List<Message> l = new List<Message>();
                l.Add(m1);
                l.Add(m2);
                return View(l); // NotFound();
            }

            var chats = await _context.Message
                .Where(m => m.UserPhoneNumber == phoneNumber)
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
            if (messages == null)
            {
                return NotFound();
            }
            ViewData["Self"] = phoneNumber;
            ViewData["Sender"] = senderPhoneNumber;
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
            return RedirectToAction(nameof(Details), new { phoneNumber = recipient, senderPhoneNumber = sender });
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
