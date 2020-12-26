using System;
using System.Collections.Generic;

namespace LIS.Core.Application.ViewModels
{
    public class RoleDetailViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<RolesPagesViewModel> RolesPagesViewModels { get; set; }
        public List<UserRoleViewModel> Users { get; set; }
    }
}
