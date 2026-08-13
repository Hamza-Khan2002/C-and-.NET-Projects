using System.ComponentModel.DataAnnotations;

namespace BanquetManagementSystem.Models
{
    public enum Role { Manager, Chef, Waiter, Decorator, Security, SoundEngineer}

    public class BanquetEmployee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required, Phone, StringLength(15, MinimumLength = 10)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Salary { get; set; }
        [Required]
        public DateOnly JoinedDate { get; set; }
    }
}
