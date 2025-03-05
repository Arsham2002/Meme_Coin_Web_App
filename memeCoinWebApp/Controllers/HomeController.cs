using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using memeCoinWebApp.Models;
using System.Linq;
using memeCoinWebApp.Data;

namespace memeCoinWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ComContext _context;

    public HomeController(ComContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        int a = _context.Fund.Count();
        int b = _context.Transfer.Count();
        int no_transaction = a + b;
        ViewData["no_transaction"] = no_transaction;

        decimal totalAmountA = _context.Fund.Sum(a => (decimal?)a.Amount) ?? 0;
        decimal totalAmountB = _context.Transfer.Sum(b => (decimal?)b.Amount) ?? 0;
        decimal totalAmount  = totalAmountA + totalAmountB;
        ViewData["totalAmount"]  = totalAmount;


        DateTime now = DateTime.Now;
        DateTime lastWeek  = now.AddDays(-7);
        DateTime lastMonth = now.AddMonths(-1);
        DateTime lastYear  = now.AddYears(-1);

        int countLastWeek  = _context.Message.Count(a => a.Timestamp >= lastWeek);
        int countLastMonth = _context.Message.Count(a => a.Timestamp >= lastMonth);
        int countLastYear  = _context.Message.Count(a => a.Timestamp >= lastYear);

        ViewData["CountLastWeek"]  = countLastWeek;
        ViewData["CountLastMonth"] = countLastMonth;
        ViewData["CountLastYear"]  = countLastYear;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
