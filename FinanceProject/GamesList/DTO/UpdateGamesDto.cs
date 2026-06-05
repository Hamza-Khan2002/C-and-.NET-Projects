namespace GamesList.DTO
{
    public class UpdateGamesDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<int> GenreIds { get; set; } = [];
    }
}
