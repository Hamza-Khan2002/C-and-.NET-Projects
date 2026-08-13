using BanquetManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BanquetManagementSystem.DTOs
{
    public class BanquetBookingDto
    {

        [Key]
        public int Id { get; set; }
        [Required, StringLength(11)]
        public string ReferenceId { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
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
        public string PackageName { get; set; } = string.Empty;
    }
}
