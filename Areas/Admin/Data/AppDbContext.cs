using Microsoft.EntityFrameworkCore;
using MVCShop.Areas.Admin.Models;

namespace MVCShop.Areas.Admin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
