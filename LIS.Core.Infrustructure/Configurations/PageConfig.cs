using LIS.Core.Domain.AggregateModels.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.Configurations
{
    public class PageConfig : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasMany(p => p.TenantPages)
                .WithOne()
                .HasForeignKey(p => p.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Pages");
        }
    }
}
