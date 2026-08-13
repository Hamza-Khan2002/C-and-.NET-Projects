using System.ComponentModel.DataAnnotations;

namespace BanquetManagementSystem.Models
{
    public class BanquetGallery
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string FilePath { get; set; } = string.Empty;
    }
}
