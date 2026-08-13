using Microsoft.EntityFrameworkCore;
using SignUp___SignIn_Form.Models;

namespace SignUp___SignIn_Form.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
        
        public DbSet<User> Users { get; set; }
    }

}
