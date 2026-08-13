namespace FinanceProject.DTO.Stock
{
    public class FMPSearchDto
    {
        public string symbol { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string currency { get; set; } = string.Empty;
        public string exchangeFullName { get; set; } = string.Empty;
        public string exchange { get; set; } = string.Empty;
    }
}
