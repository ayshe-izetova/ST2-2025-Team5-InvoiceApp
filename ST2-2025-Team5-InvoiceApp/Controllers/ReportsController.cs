using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST2_2025_Team5_InvoiceApp.Models;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ST2_2025_Team5_InvoiceApp.Services;
using System.Globalization;

namespace ST2_2025_Team5_InvoiceApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var invoices = _context.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.IssueDate)
                .Take(10)
                .ToList();

            var totalSum = _context.Invoices
                .SelectMany(i => i.Items)
                .Sum(x => x.UnitPrice * x.Quantity);

            ViewBag.TotalInvoices = _context.Invoices.Count();
            ViewBag.PaidInvoices = _context.Invoices.Count(i => i.Status == "Paid");
            ViewBag.PendingInvoices = _context.Invoices.Count(i => i.Status == "Pending");
            ViewBag.TotalSum = $"{totalSum:N2} лв"; // 💰 форматирано

            return View(invoices);
        }

        // 🧾 PDF Export
        public IActionResult ExportToPdf()
        {
            var invoices = _context.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.IssueDate)
                .Take(10)
                .ToList();

            using var ms = new MemoryStream();
            var document = new Document(PageSize.A4, 40, 40, 40, 40);
            PdfWriter.GetInstance(document, ms);
            document.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            document.Add(new Paragraph("Invoice Report", titleFont));
            document.Add(new Paragraph($"Generated on: {DateTime.Now:dd.MM.yyyy HH:mm}", normalFont));
            document.Add(new Paragraph(" "));

            var totalInvoices = _context.Invoices.Count();
            var paid = _context.Invoices.Count(i => i.Status == "Paid");
            var pending = _context.Invoices.Count(i => i.Status == "Pending");
            var totalSum = _context.Invoices.SelectMany(i => i.Items).Sum(x => x.UnitPrice * x.Quantity);

            // 💰 всички стойности форматирани
            document.Add(new Paragraph($"Total invoices: {totalInvoices}", normalFont));
            document.Add(new Paragraph($"Paid invoices: {paid}", normalFont));
            document.Add(new Paragraph($"Pending invoices: {pending}", normalFont));
            document.Add(new Paragraph($"Total sum: {totalSum.ToString("N2", CultureInfo.InvariantCulture)} лв", normalFont));

            document.Close();

            return File(ms.ToArray(), "application/pdf", $"InvoiceReport_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        // 🧠 Simulated AI Summary (локален модел)
        [HttpGet]
        public IActionResult GenerateSummary()
        {
            var total = _context.Invoices.Count();
            var paid = _context.Invoices.Count(i => i.Status == "Paid");
            var pending = _context.Invoices.Count(i => i.Status == "Pending");
            var sum = _context.Invoices.SelectMany(i => i.Items).Sum(x => x.UnitPrice * x.Quantity);

            var lastMonth = DateTime.Now.AddMonths(-1);
            var monthly = _context.Invoices
                .AsEnumerable() // клиентска обработка
                .Count(i => i.IssueDate.HasValue &&
                            i.IssueDate.Value.ToDateTime(TimeOnly.MinValue) > lastMonth);

            var sb = new StringBuilder();
            sb.AppendLine("🤖 AI Summary Report:");
            sb.AppendLine($"You currently have {total} invoices in the system.");
            sb.AppendLine($"{paid} are marked as Paid, and {pending} are still Pending.");
            sb.AppendLine($"Total revenue generated so far: {sum.ToString("N2", CultureInfo.InvariantCulture)}");
            sb.AppendLine($"In the last 30 days, you issued {monthly} invoices.");
            sb.AppendLine();
            sb.AppendLine("💡 Business Insight:");
            if (paid > pending)
                sb.AppendLine("Most invoices are successfully paid on time — excellent performance!");
            else
                sb.AppendLine("Warning: You have more pending invoices — consider reminding clients.");

            return Json(new { summary = sb.ToString().Replace("\n", "<br>") });
        }

        [HttpGet]
        public IActionResult GetMonthlyStats()
        {
            var data = _context.Invoices
                .AsEnumerable()
                .Where(i => i.IssueDate.HasValue)
                .GroupBy(i => new { i.IssueDate!.Value.Year, i.IssueDate!.Value.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Month:D2}.{g.Key.Year}",
                    Count = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToList();

            return Json(data);
        }
    }
}
