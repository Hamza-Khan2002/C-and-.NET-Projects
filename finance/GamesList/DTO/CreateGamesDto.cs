using System.ComponentModel.DataAnnotations;

namespace GamesList.DTO
{
    public class CreateGamesDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly CreatedOn { get; set; }
        public List<int> GenreIds { get; set; } = [];
    }
}
