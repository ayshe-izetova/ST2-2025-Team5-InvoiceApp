using Microsoft.EntityFrameworkCore;

namespace ST2_2025_Team5_InvoiceApp.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
