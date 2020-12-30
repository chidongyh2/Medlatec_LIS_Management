using Medlatec.Infrastructure.SeedWorks;
using System;
using System.ComponentModel.DataAnnotations;

namespace Medlatec.Infrastructure.Domain.SystemAggregate
{
    public class District: Entity
    {
        /// <summary>
        /// Tên Quận/Huyện.
        /// </summary>
        [Required, MaxLength(150)]
        public string Name { get; private set; }
        
        /// <summary>
        /// Mã Tỉnh thành.
        /// </summary>
        [Required]
        public Guid ProvinceId { get; private set; }

        /// <summary>
        /// Tên Tỉnh/Thành.
        /// </summary>
        [Required, MaxLength(150)]
        public string ProvinceName { get; private set; }

        /// <summary>
        /// Mã ngôn ngữ.
        /// </summary>
        [Required, MaxLength(100)]
        public string Culture { get; private set; }

        /// <summary>
        /// Tên không dấu phục vụ tìm kiếm.
        /// </summary>
        [Required, MaxLength(150)]
        public string UnsignName { get; private set; }
        public virtual Province Province { get; private set; }

        public District()
        {
            Culture = "VN";
            UnsignName = "";
        }
    }

}
