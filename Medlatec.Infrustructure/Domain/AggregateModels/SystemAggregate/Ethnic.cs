using System;
using System.ComponentModel.DataAnnotations;

namespace Medlatec.Infrastructure.Domain.SystemAggregate
{
    public class Ethnic
    {
        /// <summary>
        /// Mã dân tộc.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên dân tộc.
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
