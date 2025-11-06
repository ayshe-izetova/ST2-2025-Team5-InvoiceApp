using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST2_2025_Team5_InvoiceApp.Models
{
    // Design Pattern: DTO / POCO (Plain Old CLR Object)
    // This class represents a simple data transfer object (DTO) used to map
    // the Invoice entity between the database (AppDbContext) and the application logic.
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Number { get; set; } = "";

        [Required]
        public string Status { get; set; } = "";

        public DateOnly? IssueDate { get; set; }
        public DateOnly? DueDate { get; set; }

        // Service details
        [Required]
        public string Service { get; set; } = "";

        [Precision(16, 2)]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        // Client details
        [Required]
        public string ClientName { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public string Phone { get; set; } = "";

        public string Address { get; set; } = "";

        // Calculated property (not stored in DB)
        [NotMapped]
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
