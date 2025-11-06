using Microsoft.EntityFrameworkCore;
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

        // Клиентски данни
        [Required]
        public string ClientName { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public string Phone { get; set; } = "";

        public string Address { get; set; } = "";

        // 🧾 Връзка 1:N с InvoiceItems
        public ICollection<InvoiceItems> Items { get; set; } = new List<InvoiceItems>();

        [NotMapped]
        public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}
