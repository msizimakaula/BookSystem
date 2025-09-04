using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Users : IdentityUser
    {
        public string Name{ get; set; }


        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
