using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Product__Application.Models;

namespace Product__Application.Context.DbContext
{
    public class DataBaseContext : IdentityDbContext<UserApp>
    {
        public DbSet<UserApp> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mobile" },
                new Category { Id = 2, Name = "Labtop" },
                new Category { Id = 3, Name = "Fashion" }

            );
        }

    }
}
