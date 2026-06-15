using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles =
            [
                new() { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "b50106a9-c9dd-46f4-ba0e-5922c0c34895" },
                new() { Id = "2", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "6f328af3-8f22-4f02-bb0b-d6719ddd5bee" }
            ];

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
