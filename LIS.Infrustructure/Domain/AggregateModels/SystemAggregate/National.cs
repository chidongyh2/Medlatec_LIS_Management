using LIS.Infrastructure.SeedWorks;
using System.Collections.Generic;

namespace LIS.Infrastructure.Domain.SystemAggregate
{
    public class National : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string UnsignName { get; private set; }
        public IList<Province> Provinces { get; private set; }
        public IList<Religion> Religions { get; private set; }
        public IList<Ethnic> Ethnics { get; private set; }
    }
}
