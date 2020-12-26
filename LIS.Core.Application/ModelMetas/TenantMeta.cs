using System.Collections.Generic;

namespace LIS.Core.Application.ModelMetas
{
    public class TenantMeta
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Note { get; set; }
        public string Logo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<int> PageIds { get; set; }
    }
}
