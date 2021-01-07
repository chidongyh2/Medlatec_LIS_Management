using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Medlatec.Core.Domain.AggregateModels.TenantAggregate;
using Medlatec.Infrastructure.Domain.AccountAggregate;
using Medlatec.Infrastructure.Domain.SystemAggregate;
using Medlatec.Infrastructure.Oracle;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Medlatec.Core.Infrastructure
{
    public class CoreDbContext : IdentityDbContext<Account, Role, Guid>, IDbContext
    {
        private readonly Lazy<EntityQueryFilterProvider> _filterProviderInitializer = new Lazy<EntityQueryFilterProvider>();
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // handler multiple tenant connection string to Here
            #region Configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

            #region Table Mapping
            builder.Entity<TenantPage>(x => x.ToTable("TenantPages"));
            builder.Entity<National>(x => x.ToTable("Nationals"));
            builder.Entity<Religion>(x => x.ToTable("Religions"));
            builder.Entity<Ethnic>(x => x.ToTable("Ethnics"));
            builder.Entity<District>(x => x.ToTable("Districts"));
            builder.Entity<Province>(x => x.ToTable("Provinces"));
            builder.Entity<RolePage>(x => x.ToTable("RolePages"));
            #endregion
        }

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(new CancellationToken());
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(this);
        }

        public IQueryable<T> RawFromSql<T>(string queryText) where T : class
        {
            return Set<T>().FromSqlRaw(queryText);
        }

        public IQueryable<T> RawFromSql<T>(string queryText, params object[] parameters) where T : class
        {
            return Set<T>().FromSqlRaw(queryText, parameters);
        }

        public QueryFilterProvider Filters => _filterProviderInitializer.Value;

        public ChangeTracker GetChangeTracker => ChangeTracker;
    }
}
