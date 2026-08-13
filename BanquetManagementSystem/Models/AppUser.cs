using Microsoft.AspNetCore.Identity;

namespace BanquetManagementSystem.Models
{
    public class AppUser : IdentityUser
    {
        public List<BanquetReview> Reviews { get; set; } = [];
        public List<BanquetBooking> Bookings { get; set; } = [];
    }
}
