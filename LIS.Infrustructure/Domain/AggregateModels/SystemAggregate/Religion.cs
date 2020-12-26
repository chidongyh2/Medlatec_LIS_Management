using LIS.Infrastructure.SeedWorks;

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
    }
}
