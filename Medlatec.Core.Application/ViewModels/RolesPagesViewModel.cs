using System;

namespace Medlatec.Core.Application.ViewModels
{
    public class RolesPagesViewModel
    {
        public Guid RoleId { get; set; }
        public int? PageId { get; set; }
        public string PageName { get; set; }
        public int? Permissions { get; set; }
        public string IdPath { get; set; }
        public int? Type { get; set; }
        public int ChildCount { get; set; }
    }
}
