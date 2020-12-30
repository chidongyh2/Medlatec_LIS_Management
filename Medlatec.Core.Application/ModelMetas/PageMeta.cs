using System.Collections.Generic;

namespace Medlatec.Core.Application.ModelMetas
{
    public class PageMeta
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public string BgColor { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public bool IsPublic { get; set; }
        public string Url { get; set; }
        public List<string> TenantIds { get; set; }
    }
}
