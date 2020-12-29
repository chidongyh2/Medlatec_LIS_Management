using LIS.Infrastructure.Domain.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LIS.Authentication.Configuration.EntityConfigurations
{
    public class AccountRoleEntityTypeConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasOne(t => t.Role)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Account)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
