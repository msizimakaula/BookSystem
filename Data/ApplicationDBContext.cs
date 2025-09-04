using BulkyBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace BulkyBook.Data
{
    public class ApplicationDBContext : IdentityDbContext<Users, IdentityRole, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Category>Categories { get; set; } //create DB set   categ table N.B name should be the same with 4 columns created in Category.cs
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
