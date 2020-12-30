using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Medlatec.Infrastructure.Oracle
{
    public abstract class QueryFilterProvider
    {
        /// <summary>
        /// Tạo một bộ lọc để phân trang (phân trang cần được thực hiện sau khi sắp xếp).
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <param name="pageAt">Trang hiện tại.</param>
        /// <param name="pageSize">Số phần tử trên 1 trang.</param>
        /// <returns></returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> Page<T>(int pageAt = 1, int pageSize = 20);

        /// <summary>
        /// Tạo một bộ lọc bao gồm (bao gồm thêm các thực thể liên quan, tuy nhiên nếu định nghĩa các thực thể chính xác thì không cần dùng đến bộ lọc này)
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <param name="paths">Đường dẫn.</param>
        /// <returns></returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> Include<T>(params string[] paths);

        /// <summary>
        /// Tạo một bộ lọc sắp xếp kết quả truy vấn
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <typeparam name="TKey">Kiểu dữ liệu của thuộc tính cần sắp xếp.</typeparam>
        /// <param name="sorter">Sorter.</param>
        /// <param name="descending">Nếu là true thì sẽ sắp xếp theo thứ tự giảm dần.</param>
        /// <returns></returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> Sort<T, TKey>(Expression<Func<T, TKey>> sorter, bool descending = false);

        /// <summary>
        /// Tạo một sắp xếp.
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <param name="descending">Nếu là true thì sẽ sắp xếp theo thứ tự giảm dần.</param>
        /// <param name="sortExprs">Danh sách tên các cột cần sắp xếp</param>
        /// <returns>IQueryable</returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> CreateSort<T>(bool descending = false, params string[] sortExprs);

        /// <summary>
        /// Tạo một sắp xếp.
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>       
        /// <param name="sortExprs">Danh sách sắp xếp Tên của cột cần sắp xếp | sắp xếp theo thứ tự tăng dần hay giảm dần</param>
        /// <returns>IQueryable</returns>
        public abstract Func<IQueryable<T>, IQueryable<T>> CreateSort<T>(Dictionary<string, bool> sortExprs);
    }
}
