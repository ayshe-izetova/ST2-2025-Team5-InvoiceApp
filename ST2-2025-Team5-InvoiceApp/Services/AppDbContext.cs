using Microsoft.EntityFrameworkCore;
using ST2_2025_Team5_InvoiceApp.Models;

namespace ST2_2025_Team5_InvoiceApp.Services
{
    // Design Pattern: Data Access Object (DAO) + Repository abstraction
    // This class represents the data access layer for the application.
    // It encapsulates all interaction with the database using Entity Framework Core.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // Table mapping for invoices
        public DbSet<Invoice> Invoices { get; set; } = null!;

        // Future entities (for extension)
        // public DbSet<Client> Clients { get; set; }
        // public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example entity configuration (optional)
            // modelBuilder.Entity<Invoice>().Property(i => i.Number).IsRequired();
        }
    }
}
