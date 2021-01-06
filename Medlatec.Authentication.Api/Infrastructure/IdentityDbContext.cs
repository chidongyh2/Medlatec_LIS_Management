using Medlatec.Infrastructure.Domain.AccountAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Medlatec.Authentication.Infrastructure
{
    public class IdentityDbContext : IdentityDbContext<Account, Role, Guid>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Table Mapping
            builder.Entity<Account>(x => x.ToTable("Accounts"));
            #endregion
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
