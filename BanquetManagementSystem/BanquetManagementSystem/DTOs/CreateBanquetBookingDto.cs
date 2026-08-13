using BanquetManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BanquetManagementSystem.DTOs
{
    public class CreateBanquetBookingDto
    {
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
        public int PackageId { get; set; }
    }
}
