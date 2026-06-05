namespace GamesList.DTO
{
    public class GamesListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly CreatedOn { get; set; }
        public List<string> Genre { get; set; } = [];
    }
}

