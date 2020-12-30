using Medlatec.Infrastructure.Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medlatec.Core.Infrastructure.EntityConfigurations
{
    public class RoleEntityTypeConfiguration: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(t => t.AccountRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
