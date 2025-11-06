using ST2_2025_Team5_InvoiceApp.Models;

namespace ST2_2025_Team5_InvoiceApp.Services
{
    // Design Pattern: DAO (Data Access Object)
    // Handles all database operations related to Invoice.
    public class InvoiceDAO
    {
        private readonly AppDbContext _context;

        public InvoiceDAO(AppDbContext context)
        {
            _context = context;
        }

        public List<Invoice> GetAll() => _context.Invoices.OrderByDescending(i => i.Id).ToList();

        public Invoice? GetById(int id) => _context.Invoices.Find(id);

        public void Add(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var inv = _context.Invoices.Find(id);
            if (inv != null)
            {
                _context.Invoices.Remove(inv);
                _context.SaveChanges();
            }
        }
    }
}
