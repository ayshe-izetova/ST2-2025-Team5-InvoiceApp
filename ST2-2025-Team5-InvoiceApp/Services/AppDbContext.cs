using Microsoft.EntityFrameworkCore;
using ST2_2025_Team5_InvoiceApp.Models;

namespace ST2_2025_Team5_InvoiceApp.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; } = null!;
    }
}
