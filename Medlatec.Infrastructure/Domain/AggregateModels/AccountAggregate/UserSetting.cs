using Medlatec.Infrastructure.SeedWorks;
using System;

namespace Medlatec.Infrastructure.Domain.AccountAggregate
{
    public class AccountSetting : ModifierTrackingEntity
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
