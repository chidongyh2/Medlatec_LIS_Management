using LIS.Infrastructure.Domain.SystemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.Configurations
{
    public class EthnicConfig : IEntityTypeConfiguration<Ethnic>
    {
        public void Configure(EntityTypeBuilder<Ethnic> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.UnsignName).IsRequired().HasMaxLength(256).IsUnicode(false);
            builder.ToTable("Ethnics").HasKey(x => x.Id);
        }
    }
}
