using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoboostTask.Models;

namespace RoboostTask.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //من التوتر نسيت اعملها بابليك :(
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 

    }
}
