using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanquetManagementSystem.Models
{
    public enum FunctionType { Wedding, Walima, Mehndi, Engagement, Corporate, Birthday, Other}
    public enum  TimeSlot { Day, Evening}    
    public enum BookingStatus { Pending, Confirmed, Cancelled}
    
    public class BanquetBooking
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(11)]
        public string ReferenceId { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required, Phone, StringLength(15, MinimumLength = 10)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public FunctionType FunctionType { get; set; }
        [Required]
        public DateOnly BookingDate { get; set; }
        [Required]
        public TimeSlot TimeSlot { get; set; }
        [Required, Range(1, 500)]
        public int GuestCount { get; set; }
        [MaxLength(500)]
        public string? SpecialNote { get; set; } = string.Empty;
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public int PackageId { get; set; }
        public int AppUserId { get; set; }

        public BanquetPackage? BanquetPackage { get; set; }
        public AppUser? User { get; set; }
    }
}
