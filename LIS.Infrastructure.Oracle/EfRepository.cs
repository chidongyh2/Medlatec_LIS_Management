using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LIS.Infrastructure.Oracle
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private DbSet<T> _entities;

        /// <summary>
        /// Khởi tạo <see cref="EfRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">Context (IDbContext).</param>
        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        private DbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

#pragma warning disable 1591
        public void Deletes(List<T> entity)
        {
            if (entity == null || !entity.Any())
                throw new ArgumentNullException(nameof(entity));

            entity.ForEach(x =>
            {
                _context.Set<T>().Attach(x);
                Entities.Remove(x);
            });
        }

        public IQueryable<T> RawReadOnly => Entities.AsNoTracking();

        public IQueryable<T> Raw => Entities;

        public List<T> Gets(bool isReadOnly,
                                    Expression<Func<T, bool>> spec = null,
                                    Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            return FindCore(isReadOnly, spec, preFilter, postFilters).ToList();
        }

        public Task<List<T>> GetsAsync(bool isReadOnly,
                                    Expression<Func<T, bool>> spec = null,
                                    Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            return FindCore(isReadOnly, spec, preFilter, postFilters).ToListAsync();
        }

        public List<T> GetsReadOnly(Expression<Func<T, bool>> spec = null,
                                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter)
        {
            return Gets(true, spec, preFilter, postFilter);
        }

        public Task<List<T>> GetsReadOnlyAsync(Expression<Func<T, bool>> spec = null,
                                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter)
        {
            return GetsAsync(true, spec, preFilter, postFilter);
        }

        public List<TOutput> GetsAs<TOutput>(Expression<Func<T, TOutput>> projector,
                                                    Expression<Func<T, bool>> spec = null,
                                                    Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            if (projector == null)
            {
                throw new ArgumentNullException(nameof(projector));
            }
            return FindCore(true, spec, preFilter, postFilters).Select(projector).ToList();
        }

        public Task<List<TOutput>> GetsAsAsync<TOutput>(Expression<Func<T, TOutput>> projector,
                                                    Expression<Func<T, bool>> spec = null,
                                                    Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            if (projector == null)
            {
                throw new ArgumentNullException(nameof(projector));
            }
            return FindCore(true, spec, preFilter, postFilters).Select(projector).ToListAsync();
        }

        public T Get(params object[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentException("no id specified");
            }
            return Entities.Find(ids);
        }

        public T Get(bool isReadOnly, Expression<Func<T, bool>> spec)
        {
            if (spec == null)
            {
                throw new ArgumentNullException(nameof(spec));
            }
            return isReadOnly ? Entities.AsNoTracking().SingleOrDefault(spec) : Entities.SingleOrDefault(spec);
        }

        public Task<T> GetAsync(bool isReadOnly, Expression<Func<T, bool>> spec)
        {
            if (spec == null)
            {
                throw new ArgumentNullException(nameof(spec));
            }
            return isReadOnly ? Entities.AsNoTracking().SingleOrDefaultAsync(spec) : Entities.SingleOrDefaultAsync(spec);
        }

        public T GetReadOnly(Expression<Func<T, bool>> spec)
        {
            return Get(true, spec);
        }

        public Task<T> GetReadOnlyAsync(Expression<Func<T, bool>> spec)
        {
            return GetAsync(true, spec);
        }

        public TOutput GetAs<TOutput>(Expression<Func<T, TOutput>> projector,
                                        Expression<Func<T, bool>> spec)
        {
            if (projector == null)
            {
                throw new ArgumentNullException(nameof(projector));
            }
            if (spec == null)
            {
                throw new ArgumentNullException(nameof(spec));
            }
            return Entities.AsNoTracking().Where(spec).Select(projector).SingleOrDefault();
        }

        public Task<TOutput> GetAsAsync<TOutput>(Expression<Func<T, TOutput>> projector,
                                        Expression<Func<T, bool>> spec)
        {
            if (projector == null)
            {
                throw new ArgumentNullException(nameof(projector));
            }
            if (spec == null)
            {
                throw new ArgumentNullException(nameof(spec));
            }
            return Entities.AsNoTracking().Where(spec).Select(projector).SingleOrDefaultAsync();
        }

        public bool Exist(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? Entities.Any() : Entities.Any(spec);
        }

        public Task<bool> ExistAsync(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? Entities.AnyAsync() : Entities.AnyAsync(spec);
        }

        public int Count(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? Entities.Count() : Entities.Count(spec);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? Entities.CountAsync() : Entities.CountAsync(spec);
        }

        public long CountLong(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? Entities.LongCount() : Entities.LongCount(spec);
        }

        public Task<long> CountLongAsync(Expression<Func<T, bool>> spec = null)
        {
            return spec == null ? Entities.LongCountAsync() : Entities.LongCountAsync(spec);
        }

        public void Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Add(entity);
        }

        public void Creates(List<T> entity)
        {
            if (entity == null || !entity.Any())
                throw new ArgumentNullException(nameof(entity));

            foreach (var item in entity)
            {
                Entities.Add(item);
            }
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<T>().Attach(entity);
            Entities.Remove(entity);
        }

        private IQueryable<T> FindCore(bool isReadOnly, Expression<Func<T, bool>> spec, Func<IQueryable<T>, IQueryable<T>> preFilter, params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            var entities = isReadOnly ? Entities.AsNoTracking() : Entities;
            var result = preFilter != null ? preFilter(entities) : entities;
            if (spec != null)
            {
                result = result.Where(spec);
            }
            foreach (var postFilter in postFilters)
            {
                if (postFilter != null)
                {
                    result = postFilter(result);
                }
            }
            return result;
        }
    }
}
