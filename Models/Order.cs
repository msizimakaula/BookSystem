using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Range(1, 2, ErrorMessage = "Quantity must be not be more than 2")]
        public int Quantity { get; set;}

        // New fields
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
