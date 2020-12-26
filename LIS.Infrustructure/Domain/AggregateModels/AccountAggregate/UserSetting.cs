using LIS.Infrastructure.SeedWorks;
using System;

namespace LIS.Infrastructure.Domain.AccountAggregate
{
    public class UserSetting : ModifierTrackingEntity
    {
        /// <summary>
        /// Mã người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Key setting
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Giá trị cấu hình.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Nhóm cấu hình theo key.
        /// </summary>
        public string GroupKey { get; set; }
    }
}
