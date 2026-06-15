using System.ComponentModel.DataAnnotations;

namespace FinanceProject.DTO.Account
{
    public class RegisterDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "User Name cannot exceed 15 characters.")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
