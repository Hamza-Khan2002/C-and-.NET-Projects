using System.ComponentModel.DataAnnotations;

namespace FinanceProject.DTO.Stock
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot exceed 10 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Company Name cannot exceed 50 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.1, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Industry cannot exceed 50 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 50000000000)]
        public long MarketCap { get; set; }
    }
}
