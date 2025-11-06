using ST2_2025_Team5_InvoiceApp.Models;

namespace ST2_2025_Team5_InvoiceApp.Services
{
    // Design Pattern: Facade
    // Provides a simplified interface to complex invoice operations.
    public class InvoiceFacade
    {
        private readonly InvoiceDAO _dao;
        private readonly LogService _logger;

        public InvoiceFacade(InvoiceDAO dao, LogService logger)
        {
            _dao = dao;
            _logger = logger;
        }

        public List<Invoice> GetAllInvoices()
        {
            _logger.Log("Fetching all invoices");
            return _dao.GetAll();
        }

        public Invoice? GetInvoice(int id)
        {
            _logger.Log($"Fetching invoice #{id}");
            return _dao.GetById(id);
        }

        public void CreateInvoice(Invoice invoice)
        {
            _logger.Log($"Creating new invoice: {invoice.Number}");
            _dao.Add(invoice);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _logger.Log($"Updating invoice #{invoice.Id}");
            _dao.Update(invoice);
        }

        public void DeleteInvoice(int id)
        {
            _logger.Log($"Deleting invoice #{id}");
            _dao.Delete(id);
        }
    }
}
