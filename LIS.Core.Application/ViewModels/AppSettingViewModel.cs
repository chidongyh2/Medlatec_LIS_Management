using LIS.Infrastructure.Models;
using LIS.Infrastructure.ViewModel;
using System.Collections.Generic;

namespace LIS.Core.Application.ViewModels
{
    public class AppSettingViewModel
    {
        public List<PageGetByUserViewModel> Pages { get; set; }
        public List<UserSetting> UserSettings { get; set; }
        public List<RolesPagesViewModel> Permissions { get; set; }
        public BriefUser CurrentUser { get; set; }
        public string LogoUrl { get; set; }
        public string DefaultAppTitle { get; set; }
    }
     
    public class UserSetting
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
