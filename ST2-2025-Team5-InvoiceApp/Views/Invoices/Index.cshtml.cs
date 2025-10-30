using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST2_2025_Team5_InvoiceApp.Services;
using ST2_2025_Team5_InvoiceApp.Models;

namespace ST2_2025_Team5_InvoiceApp.Views.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext context;

        public List<Invoice> InvoiceList { get; set; } = new();

        public IndexModel(AppDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            InvoiceList = context.Invoices.OrderByDescending(i => i.Id).ToList();
        }
    }
}
