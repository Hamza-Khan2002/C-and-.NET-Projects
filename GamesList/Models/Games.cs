namespace GamesList.Models
{
    public class Games
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly CreatedOn { get; set; }

        public ICollection<Genre> Genre { get; set; } = [];


    }
}
