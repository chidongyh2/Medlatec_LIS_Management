using System;
using System.Collections.Generic;
using System.Text;

namespace Medlatec.Infrastructure.Models
{
    public class ApiUrlSettings
    {
        public string CoreApiUrl { get; set; }
        public string Authority { get; set; }
        public string CheckPermissionUrl { get; set; }
        public string NotificationApiUrl { get; set; }
    }
}
