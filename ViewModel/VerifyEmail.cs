using System.ComponentModel.DataAnnotations;

namespace BulkyBook.ViewModel
{
    public class VerifyEmail
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

    }
}
