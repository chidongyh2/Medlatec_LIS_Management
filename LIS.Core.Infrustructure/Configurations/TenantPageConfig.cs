using LIS.Core.Domain.AggregateModels.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.Configurations
{
    public class TenantPageConfig : IEntityTypeConfiguration<TenantPage>
    {
        public void Configure(EntityTypeBuilder<TenantPage> builder)
        {
            builder.Property(x => x.TenantId).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.PageId).IsRequired();
            builder.Property(x => x.IsDelete).IsRequired();
            builder.ToTable("TenantsPages").HasKey(x => new { x.TenantId, x.PageId });
        }
    }
}
