using System.Collections.Generic;

namespace Medlatec.Core.Application.ModelMetas
{
    public class SubjectTagMeta
    {
        public string TenantId { get; set; }
        public string CreatorId { get; set; }
        public string CreatorFullName { get; set; }
        public string CreatorAvata { get; set; }
        public int Type { get; set; }
        public string SubjectId { get; set; }
        public string LanguageId { get; set; }
        public List<TagMeta> Tags { get; set; }

    }
}
