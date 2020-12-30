using System;
using System.Collections.Generic;
using System.Text;

namespace Medlatec.Core.Application.ViewModels
{
    public class TenantSearchViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
