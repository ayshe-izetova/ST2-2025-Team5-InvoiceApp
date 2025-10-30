using Microsoft.AspNetCore.Mvc;
using ST2_2025_Team5_InvoiceApp.Models;
using ST2_2025_Team5_InvoiceApp.Services;

namespace ST2_2025_Team5_InvoiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var invoiceList = _context.Invoices.OrderByDescending(i => i.Id).ToList();
            return View(invoiceList);
        }
    }
}
