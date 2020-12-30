namespace Medlatec.Infrastructure.Models
{
    public abstract class SearchQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
    }
}
