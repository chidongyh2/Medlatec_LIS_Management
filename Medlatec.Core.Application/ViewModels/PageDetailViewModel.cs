using System.Collections.Generic;

namespace Medlatec.Core.Application.ViewModels
{
    public class PageDetailViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public string BgColor { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public int ChildCount { get; set; }
        public string Url { get; set; }
        public List<PageTranslationSearchViewModel> PageTranslation { get; set; }
        public List<string> TenantIds { get; set; }
    }
}
