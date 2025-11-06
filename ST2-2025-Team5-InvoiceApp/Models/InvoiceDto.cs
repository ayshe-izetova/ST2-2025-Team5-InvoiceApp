using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ST2_2025_Team5_InvoiceApp.Models
{
    public class InvoiceDto
    {
        [Required]
        public string Number { get; set; } = "";
        [Required]
        public string Status { get; set; } = "";
        public DateOnly? IssueDate { get; set; }
        public DateOnly? DueDate { get; set; }

        //service details
        [Required]
        public string Service { get; set; } = "";
        [Range(1,999999, ErrorMessage="Unit Price is not valid!")]
        public decimal UnitPrice { get; set; }
        [Range (1,99)]
        public int Quantity { get; set; }

        //client details
       // [Required(ErrorMessage ="Client Name is required")]
       // public string ClientName { get; set; } = "";
       // [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
       // public string Email { get; set; } = "";
       // [Phone]
       // public string Phone { get; set; } = "";
       // public string Address { get; set; } = "";
    }
}
