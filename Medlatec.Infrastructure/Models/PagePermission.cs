using Medlatec.Infrastructure.Constants;

namespace Medlatec.Infrastructure.Models
{
    public class PagePermission
    {
        public PageId PageId { get; set; }
        public Permission[] Permissions { get; set; }
        public PagePermission(PageId pageId, Permission[] permissions)
        {
            PageId = pageId;
            Permissions = permissions;
        }
    }
}
