using System.Collections.Generic;

namespace Medlatec.Core.Application.ModelMetas
{
    public class PageMeta
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public bool IsShowSidebar { get; set; }
        public string Url { get; set; }
        public List<string> TenantIds { get; set; }
    }
}
