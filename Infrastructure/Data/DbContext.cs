using DemoMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }// it creates Product named database table on database
        public DbSet<User> Users { get; set; }// it creates User named database table on database
    }

}
