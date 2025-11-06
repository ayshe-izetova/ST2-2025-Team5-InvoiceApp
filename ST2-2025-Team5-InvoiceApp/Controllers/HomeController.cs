using Microsoft.AspNetCore.Mvc;
using ST2_2025_Team5_InvoiceApp.Models;
using ST2_2025_Team5_InvoiceApp.Services;

namespace ST2_2025_Team5_InvoiceApp.Controllers
{
    // Design Pattern: MVC Controller (Presentation Layer)
    // This class handles user requests and delegates data operations
    // to the InvoiceFacade (which internally uses DAO and Singleton Logger).
    public class HomeController : Controller
    {
        private readonly InvoiceFacade _facade;

        public HomeController(InvoiceFacade facade)
        {
            _facade = facade;
        }

        // 📄 Списък с всички фактури
        public IActionResult Index()
        {
            var invoices = _facade.GetAllInvoices();
            return View(invoices);
        }

        // 🟩 GET: /Home/Create
        [HttpGet]
        public IActionResult Create() => View(new Invoice());

        // 🟦 POST: /Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _facade.CreateInvoice(model);
            return RedirectToAction(nameof(Index));
        }

        // 🟡 GET: /Home/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var invoice = _facade.GetInvoice(id);
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

            _facade.UpdateInvoice(model);
            return RedirectToAction(nameof(Index));
        }

        // 🗑 GET: /Home/Delete/{id}
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var invoice = _facade.GetInvoice(id);
            if (invoice == null)
                return RedirectToAction(nameof(Index));

            return View(invoice);
        }

        // 🗑 POST: /Home/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _facade.DeleteInvoice(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
