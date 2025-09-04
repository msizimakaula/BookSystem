using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Title { get; set; }


        [DisplayName("Display Order")] //naming it your way not the model way
        [Range(0,100, ErrorMessage ="Display Order must be between 1 and 100")]
       
        public int DisplayOrder { get; set; }
       
        public DateTime CreatedDateTime { get; set; } = DateTime.Now; //automatically sets datetime as default

        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
        public double Price { get; set; } // Price per category

        // Computed property (not mapped to DB)
        [DisplayName("Total Price")]
        public double TotalPrice
        {
            get { return Price * DisplayOrder; } // Computed automatically
        }

        [Required]
        [DisplayName("Book Status")]
        public string Status { get; set; } = "Out of stock";
    }
}
  