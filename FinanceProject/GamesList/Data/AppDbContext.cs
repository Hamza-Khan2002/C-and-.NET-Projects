using GamesList.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesList.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().
                HasData(
                    new Genre { Id = 1, Name = "Fighting" },
                    new Genre { Id = 2, Name = "Survival" },
                    new Genre { Id = 3, Name = "RolePlaying" },
                    new Genre { Id = 4, Name = "Kids&Family" },
                    new Genre { Id = 5, Name = "Strategy" }
                );
           }

        public DbSet<Games> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
