using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var invoices = _context.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.Id)
                .ToList();

            return View(invoices);
        }

        // 🟢 GET: /Home/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Invoice());
        }

        // 🟩 POST: /Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice model)
        {
            if (ModelState.IsValid)
            {
                _context.Invoices.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // ✏️ GET: /Home/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
                return RedirectToAction("Index");

            return View(invoice);
        }

        // 💾 POST: /Home/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == model.Id);

            if (existing == null)
                return RedirectToAction("Index");

            // Обновяване на основните данни
            _context.Entry(existing).CurrentValues.SetValues(model);

            // Изчистване на старите елементи
            _context.InvoiceItems.RemoveRange(existing.Items);

            // Добавяне на новите
            foreach (var item in model.Items)
            {
                existing.Items.Add(item);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // 🗑️ GET: Home/Delete/{id}
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
                return RedirectToAction(nameof(Index));

            return View(invoice);
        }

        // 🗑️ POST: Home/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
                return RedirectToAction(nameof(Index));

            _context.InvoiceItems.RemoveRange(invoice.Items);
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
