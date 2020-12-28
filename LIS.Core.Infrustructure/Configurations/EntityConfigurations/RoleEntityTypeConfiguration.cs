using LIS.Infrastructure.Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Core.Infrastructure.EntityConfigurations
{
    public class RoleEntityTypeConfiguration: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(t => t.AccountRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Roles").HasKey(x => x.Id);
        }
    }
}
