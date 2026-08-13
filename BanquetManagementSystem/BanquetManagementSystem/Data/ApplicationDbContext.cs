using BanquetManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BanquetManagementSystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<BanquetBooking> BanquetBookings { get; set; }
        public DbSet<BanquetEmployee> BanquetEmployees { get; set; }
        public DbSet<BanquetGallery> BanquetGalleries { get; set; }
        public DbSet<BanquetPackage> BanquetPackages { get; set; }
        public DbSet<BanquetReview> BanquetReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles =
            [
                new() {Id = "1", Name = "Admin", NormalizedName = "ADMIN"},
                new() {Id = "2", Name = "User", NormalizedName = "USER"}
            ];

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
