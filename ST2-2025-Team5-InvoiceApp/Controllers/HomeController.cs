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
            var invoiceList = _context.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.Id)
                .ToList();

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
            // Защита от празни редове и null списъци
            model.Items ??= new List<InvoiceItems>();

            model.Items = model.Items
                .Where(it => !string.IsNullOrWhiteSpace(it.Description)
                             && it.Quantity > 0
                             && it.UnitPrice >= 0)
                .ToList();

            if (string.IsNullOrWhiteSpace(model.ClientName))
                ModelState.AddModelError("ClientName", "Client name is required.");

            if (!ModelState.IsValid)
                return View(model);

            foreach (var item in model.Items)
                item.Invoice = model;

            _context.Invoices.Add(model);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // 🟡 GET: /Home/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id);

            if (invoice == null)
                return RedirectToAction(nameof(Index));

            return View(invoice);
        }

        // 🟢 POST: /Home/Edit
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
                return RedirectToAction(nameof(Index));

            // Обновяваме основните полета
            existing.Number = model.Number;
            existing.Status = model.Status;
            existing.IssueDate = model.IssueDate;
            existing.DueDate = model.DueDate;

            // ✅ Обновяваме вградените клиентски данни
            existing.ClientName = model.ClientName;
            existing.ClientEmail = model.ClientEmail;
            existing.ClientPhone = model.ClientPhone;
            existing.ClientAddress = model.ClientAddress;

            // Обновяваме артикулите (по-просто: изтриваме и добавяме)
            _context.InvoiceItems.RemoveRange(existing.Items);
            foreach (var item in model.Items)
            {
                item.InvoiceId = existing.Id;
                _context.InvoiceItems.Add(item);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // 🗑️ GET: /Home/Delete/{id}
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

        // 🗑️ POST: /Home/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.Id == id);

            if (invoice != null)
            {
                _context.InvoiceItems.RemoveRange(invoice.Items);
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
