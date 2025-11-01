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

        // 📄 Списък с всички фактури
        public IActionResult Index()
        {
            var invoiceList = _context.Invoices.OrderByDescending(i => i.Id).ToList();
            return View(invoiceList);
        }

        // 🟩 GET: /Home/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Invoice());
        }

        // 🟦 POST: /Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice model)
        {
            if (ModelState.IsValid)
            {
                _context.Invoices.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // връща се към списъка след запис
            }

            // ако има грешки във формата — остава на Create view
            return View(model);
        }
    }
}
