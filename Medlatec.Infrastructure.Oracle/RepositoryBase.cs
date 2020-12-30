using System;

namespace Medlatec.Infrastructure.Oracle
{
    public class RepositoryBase : IDisposable
    {
        protected IDbContext Context;

        /// <summary>
        /// Khởi tạo class<see cref="RepositoryBase"/>
        /// </summary>
        /// <param name="context">Context.</param>
        protected RepositoryBase(IDbContext context)
        {
            Context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context = null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
