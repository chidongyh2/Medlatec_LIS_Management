namespace Medlatec.Infrastructure.Models
{
    public abstract class SearchQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string Sort { get; set; }
        public int Skip => ((Page - 1) * PageSize);
    }
}
