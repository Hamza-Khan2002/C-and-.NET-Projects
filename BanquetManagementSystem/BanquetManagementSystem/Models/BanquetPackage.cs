using System.ComponentModel.DataAnnotations;

namespace BanquetManagementSystem.Models
{
    public class BanquetPackage
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Guests { get; set; }
    }
}
