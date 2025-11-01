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

        // 🟡 GET: /Home/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        // 🟢 POST: /Home/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existing = _context.Invoices.Find(model.Id);
            if (existing == null)
            {
                return RedirectToAction("Index");
            }

            existing.Number = model.Number;
            existing.Status = model.Status;
            existing.IssueDate = model.IssueDate;
            existing.DueDate = model.DueDate;
            existing.Service = model.Service;
            existing.UnitPrice = model.UnitPrice;
            existing.Quantity = model.Quantity;
            existing.ClientName = model.ClientName;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.Address = model.Address;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
