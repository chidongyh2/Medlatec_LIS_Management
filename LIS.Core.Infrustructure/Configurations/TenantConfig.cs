using LIS.Core.Domain.AggregateModels.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.Configurations
{
    public class TenantConfig : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasMany(t => t.TenantPages)
                .WithOne()
                .HasForeignKey(t => t.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Tenants");
        }
    }
}
