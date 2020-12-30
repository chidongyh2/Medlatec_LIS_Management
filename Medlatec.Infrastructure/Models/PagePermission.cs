using Medlatec.Infrastructure.Constants;

namespace Medlatec.Infrastructure.Models
{
    public class PagePermission
    {
        public PageContants PageId { get; set; }
        public Permission[] Permissions { get; set; }
        public PagePermission(PageContants pageId, Permission[] permissions)
        {
            PageId = pageId;
            Permissions = permissions;
        }
    }
}
