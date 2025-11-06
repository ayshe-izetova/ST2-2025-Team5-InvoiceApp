using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST2_2025_Team5_InvoiceApp.Models
{
    public class InvoiceItems
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = "";

        [Precision(16, 2)]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Total => UnitPrice * Quantity;

        // 🔗 Връзка към фактурата
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
