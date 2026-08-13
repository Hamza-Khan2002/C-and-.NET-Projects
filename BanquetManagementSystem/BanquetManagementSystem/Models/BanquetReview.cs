using System.ComponentModel.DataAnnotations;

namespace BanquetManagementSystem.Models
{
    public class BanquetReview
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public FunctionType FunctionType { get; set; }
        [Required, Range(1, 5)]
        public int Rating { get; set; }
        [Required, StringLength(300, MinimumLength = 10)]
        public string Review { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;

        public AppUser? User { get; set; }
    }
}
