using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class User
    {
        [Key]
        public string libraryNumber { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Admin / Librarian
    }
}

