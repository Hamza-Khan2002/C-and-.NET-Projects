using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceProject.Models
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}
