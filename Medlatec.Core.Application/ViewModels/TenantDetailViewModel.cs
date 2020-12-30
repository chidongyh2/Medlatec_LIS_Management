using System.Collections.Generic;

namespace Medlatec.Core.Application.ViewModels
{
    public class TenantDetailViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UnsignName { get; set; }
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
