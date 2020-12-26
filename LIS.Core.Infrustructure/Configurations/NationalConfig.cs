using LIS.Infrastructure.Domain.SystemAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.Configurations
{
    public class NationalConfig : IEntityTypeConfiguration<National>
    {
        public void Configure(EntityTypeBuilder<National> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.ToTable("Nationals").HasKey(x => x.Id);
        }
    }
}
