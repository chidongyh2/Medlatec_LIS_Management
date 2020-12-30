using Medlatec.Infrastructure.SeedWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Medlatec.Infrastructure.Domain.SystemAggregate
{
    public class Province: Entity
    {
        [Required, MaxLength(250)]
        public string Name { get; private set; }
        [Required, MaxLength(250)]
        public string UnsignName { get; private set; }
        /// <summary>
        /// Mã quốc gia.
        /// </summary>
        public Guid? NationalId { get; private set; }
        public virtual National National { get; private set; }
        public virtual IList<District> Districts { get; private set; }
    }

}
