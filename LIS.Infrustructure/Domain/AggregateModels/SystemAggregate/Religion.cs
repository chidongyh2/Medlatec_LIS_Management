using LIS.Infrastructure.SeedWorks;
using System;

namespace LIS.Infrastructure.Domain.SystemAggregate
{
    public class Religion : Entity
    {
        /// <summary>
        /// Tên tôn giáo.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tên không dấu tìm kiếm.
        /// </summary>
        public string UnsignName { get; set; }
        public Guid NationalId { get; private set; }
        public virtual National National { get; private set; }
    }
}
