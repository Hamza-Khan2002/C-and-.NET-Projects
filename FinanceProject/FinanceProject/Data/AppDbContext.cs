using FinanceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
