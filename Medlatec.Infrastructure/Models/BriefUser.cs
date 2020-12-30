using Medlatec.Infrastructure.Constants;
using System;

namespace Medlatec.Infrastructure.Models
{
    public class BriefUser
    {
        /// <summary>
        /// Mã trùng với mã của tài khoản.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã khách hàng (Công ty) sử dụng hệ thống.
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Tên đăng nhập.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Họ tên.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Ảnh đại diện.
        /// </summary>
        public string ProfileImageUrl { get; set; }

        public TitlePrefixing TitlePrefixing { get; set; }
    }
}
