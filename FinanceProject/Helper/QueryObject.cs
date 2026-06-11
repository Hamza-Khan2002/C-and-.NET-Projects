namespace FinanceProject.Helper
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageSize { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}

