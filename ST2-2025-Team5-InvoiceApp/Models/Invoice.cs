using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST2_2025_Team5_InvoiceApp.Models
{
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

        // ✅ Вградени клиентски полета (в самата фактура)
        [Required, StringLength(100)]
        public string ClientName { get; set; } = "";

        [EmailAddress, StringLength(256)]
        public string? ClientEmail { get; set; }   // по желание може и Required

        [Phone, StringLength(50)]
        public string? ClientPhone { get; set; }

        [StringLength(200)]
        public string? ClientAddress { get; set; }

        // 🧾 1:N Items
        public ICollection<InvoiceItems> Items { get; set; } = new List<InvoiceItems>();

        [NotMapped]
        public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);

        // ❌ махаме старите
        // public int? ClientId { get; set; }
        // public Client? Client { get; set; }
    }
}
