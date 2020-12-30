using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Medlatec.Infrastructure.Oracle
{
    public interface IDbContext
    {
        /// <summary>
        /// Database
        /// </summary>
        Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }

        /// <summary>
        /// Lưu lại thay đổi của các thực thể trong context
        /// </summary>
        /// <returns>Số bản ghi được thay đổi</returns>
        int SaveChanges();


        /// <summary>
        /// Lưu lại thay đổi của các thực thể trong context
        /// </summary>
        /// <returns>Số bản ghi được thay đổi</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Lấy ra DbSet cho các kiểu thực thể
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <param name="T">DbSet của kiểu thực thể</param>
        /// <returns></returns>
        DbSet<T> Set<T>() where T : class;

        /// <summary>
        /// Lấy ra các repository cho các kiểu thực thể
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <returns>Repository của kiểu thực thể</returns>
        IRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// Entry object for update all change multiple properties.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry Entry(object entity);

        /// <summary>
        /// Entry object for update all change multiple properties. with generic entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<T> Entry<T>(T entity) where T : class;

        /// <summary>
        /// Tạo ra raw Sql query.
        /// </summary>
        /// <value>Các bộ lọc.</value>
        IQueryable<T> RawFromSql<T>(string queryText) where T : class;

        /// <summary>
        /// Tạo ra raw Sql query.
        /// </summary>
        /// <value>Các bộ lọc.</value>
        IQueryable<T> RawFromSql<T>(string queryText, params object[] parameters) where T : class;

        /// <summary>
        /// Các bộ lọc cho từng nguồn dữ liệu
        /// </summary>
        /// <value>Các bộ lọc.</value>
        QueryFilterProvider Filters { get; }

        ///// <summary>
        ///// Sử dụng câu query sql để truy vấn dữ liệu
        ///// </summary>
        ///// <param name="queryText">Câu truy vấn.</param>
        ///// <param name="parameters">Các tham số.</param>
        ///// <returns>Danh sách kết quả truy vấn</returns>        
        //List<T> RawQuery<T>(string queryText, params object[] parameters);

        ///// <summary>
        ///// Sử dụng câu query sql để truy vấn dữ liệu
        ///// </summary>
        ///// <param name="queryText">Câu truy vấn.</param>
        ///// <param name="parameters">Các tham số.</param>
        ///// <returns>Danh sách kết quả truy vấn</returns>
        //Task<List<T>> RawQueryAsync<T>(string queryText, params object[] parameters);

        ///// <summary>
        ///// Sử dụng câu query sql để thay đổi dữ liệu
        ///// </summary>
        ///// <param name="commandText">Câu truy vấn.</param>
        ///// <param name="parameters">Các tham số.</param>
        ///// <returns>Số bản ghi được thay đổi</returns>
        //int RawModify(string commandText, params object[] parameters);

        ///// <summary>
        ///// Sử dụng câu query sql để thay đổi dữ liệu
        ///// </summary>
        ///// <param name="commandText">Câu truy vấn.</param>
        ///// <param name="parameters">Các tham số.</param>
        ///// <returns>Số bản ghi được thay đổi</returns>
        //Task<int> RawModifyAsync(string commandText, params object[] parameters);

        ///// <summary>
        ///// Sử dụng câu query sql để truy vấn dữ liệu (lấy ra dữ liệu của cột đầu tiên dòng đầu tiên)
        ///// </summary>
        ///// <param name="commandText">Câu truy vấn.</param>
        ///// <param name="parameters">Các tham số.</param>
        ///// <returns>Dữ liệu của cột đầu tiên dòng đầu tiên</returns>
        //object RawScalar(string commandText, params object[] parameters);

        ///// <summary>
        ///// Sử dụng câu query sql để truy vấn dữ liệu (lấy ra dữ liệu của cột đầu tiên dòng đầu tiên)
        ///// </summary>
        ///// <param name="commandText">Câu truy vấn.</param>
        ///// <param name="parameters">Các tham số.</param>
        ///// <returns>Dữ liệu của cột đầu tiên dòng đầu tiên</returns>
        //Task<object> RawScalarAsync(string commandText, params object[] parameters);

        ///// <summary>
        ///// Các bộ lọc cho từng nguồn dữ liệu
        ///// </summary>
        ///// <value>Các bộ lọc.</value>
        //QueryFilterProvider Filters { get; }        
    }
}
