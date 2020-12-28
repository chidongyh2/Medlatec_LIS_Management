using LIS.Core.Domain.AggregateModels.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.Configurations
{
    public class TenantPageConfig : IEntityTypeConfiguration<TenantPage>
    {
        public void Configure(EntityTypeBuilder<TenantPage> builder)
        {
            builder.HasOne(t => t.Tenant)
                .WithMany(t => t.TenantPages)
                .HasForeignKey(t => t.TenantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Page)
                .WithMany(t => t.TenantPages)
                .HasForeignKey(t => t.PageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
