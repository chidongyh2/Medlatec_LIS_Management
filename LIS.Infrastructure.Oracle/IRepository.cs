using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LIS.Infrastructure.Oracle
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Tìm tất cả các phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="isReadOnly">True: Nếu kết quả trả ra chỉ để đọc</param>
        /// <param name="spec">Điều kiện tìm kiếm. Điều kiện có thể là Id > 1, Content != null... Khi điều kiện là null thì sẽ trả ra tất cả các phần tử</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể</returns>
        List<T> Gets(bool isReadOnly,
                            Expression<Func<T, bool>> spec = null,
                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Tìm tất cả các phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="isReadOnly">True: Nếu kết quả trả ra chỉ để đọc</param>
        /// <param name="spec">Điều kiện tìm kiếm. Điều kiện có thể là Id > 1, Content != null... Khi điều kiện là null thì sẽ trả ra tất cả các phần tử</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể</returns>
        Task<List<T>> GetsAsync(bool isReadOnly,
                            Expression<Func<T, bool>> spec = null,
                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Tìm tất cả các phần tử phù hợp với điều kiện truyền vào. Kết quả này chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện tìm kiếm. Điều kiện có thể là Id > 1, Content != null... Khi điều kiện là null thì sẽ trả ra tất cả các phần tử</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể</returns>
        List<T> GetsReadOnly(Expression<Func<T, bool>> spec = null,
                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Tìm tất cả các phần tử phù hợp với điều kiện truyền vào. Kết quả này chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện tìm kiếm. Điều kiện có thể là Id > 1, Content != null... Khi điều kiện là null thì sẽ trả ra tất cả các phần tử</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể</returns>
        Task<List<T>> GetsReadOnlyAsync(Expression<Func<T, bool>> spec = null,
                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Tìm tất cả các phần tử phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp. Kết quả này chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện tìm kiếm. Điều kiện có thể là Id > 1, Content != null... Khi điều kiện là null thì sẽ trả ra tất cả các phần tử</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        List<TOutput> GetsAs<TOutput>(Expression<Func<T, TOutput>> projector,
                                                Expression<Func<T, bool>> spec = null,
                                                Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                                params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Tìm tất cả các phần tử phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp. Kết quả này chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện tìm kiếm. Điều kiện có thể là Id > 1, Content != null... Khi điều kiện là null thì sẽ trả ra tất cả các phần tử</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        Task<List<TOutput>> GetsAsAsync<TOutput>(Expression<Func<T, TOutput>> projector,
                                                Expression<Func<T, bool>> spec = null,
                                                Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                                params Func<IQueryable<T>, IQueryable<T>>[] postFilter);

        /// <summary>
        /// Tìm một phần tử bởi các thuộc tính nhận dạng (Id, Key) (Chỉ truy vấn db 1 lần đầu tiên, lần sau sẽ gọi từ cache)
        /// </summary>
        /// <param name="ids">Các giá trị của thuộc tính nhận dạng</param>
        /// <returns>Kiểu thực thể</returns>
        T Get(params object[] ids);

        /// <summary>
        /// Tìm một phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="isReadOnly">True: Nếu kết quả trả ra chỉ để đọc</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Kiểu thực thể</returns>
        T Get(bool isReadOnly, Expression<Func<T, bool>> spec);

        /// <summary>
        /// Tìm một phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="isReadOnly">True: Nếu kết quả trả ra chỉ để đọc</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Kiểu thực thể</returns>
        Task<T> GetAsync(bool isReadOnly, Expression<Func<T, bool>> spec);

        /// <summary>
        /// Tìm một phần tử bởi các thuộc tính nhận dạng (Id, Key). Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns>Kiểu thực thể</returns>
        T GetReadOnly(Expression<Func<T, bool>> spec);

        /// <summary>
        /// Tìm một phần tử bởi các thuộc tính nhận dạng (Id, Key). Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns>Kiểu thực thể</returns>
        Task<T> GetReadOnlyAsync(Expression<Func<T, bool>> spec);

        /// <summary>
        /// Tìm một phần tử phù hợp với điều kiện truyền vào, dữ liệu của phần tử đó sẽ được chuyển sang 1 kiểu khác. Kết quả chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu của đầu ra.</typeparam>
        /// <param name="projector">Công cụ ánh xạ.</param>
        /// <param name="specs">Điều kiện</param>
        /// <returns>Kiểu thực thể được ánh xạ</returns>
        TOutput GetAs<TOutput>(Expression<Func<T, TOutput>> projector,
                                Expression<Func<T, bool>> specs = null);

        /// <summary>
        /// Tìm một phần tử phù hợp với điều kiện truyền vào, dữ liệu của phần tử đó sẽ được chuyển sang 1 kiểu khác. Kết quả chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu của đầu ra.</typeparam>
        /// <param name="projector">Công cụ ánh xạ.</param>
        /// <param name="specs">Điều kiện</param>
        /// <returns>Kiểu thực thể được ánh xạ</returns>
        Task<TOutput> GetAsAsync<TOutput>(Expression<Func<T, TOutput>> projector,
                                Expression<Func<T, bool>> specs = null);

        /// <summary>
        /// Xác định xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Trả về true nếu tìm thấy và ngược lại</returns>
        bool Exist(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Xác định xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Trả về true nếu tìm thấy và ngược lại</returns>
        Task<bool> ExistAsync(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Tính số lượng các phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Tính số lượng các phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Tính số lượng các phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        long CountLong(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Tính số lượng các phần tử phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        Task<long> CountLongAsync(Expression<Func<T, bool>> spec = null);

        /// <summary>
        /// Tạo ra một phần tử mới trong db
        /// </summary>
        /// <param name="entity">Thực thể.</param>
        void Create(T entity);

        /// <summary>
        /// Tạo ra một danh sách các phần tử mới trong db
        /// </summary>
        /// <param name="entity">Thực thể.</param>
        void Creates(List<T> entity);

        /// <summary>
        /// Xóa một phần tử trong db
        /// </summary>
        /// <param name="entity">Thực thể.</param>
        void Delete(T entity);

        /// <summary>
        /// Xóa một danh sách phần tử trong db
        /// </summary>
        /// <param name="entity">Danh sach thực thể.</param>
        void Deletes(List<T> entity);

        /// <summary>
        /// Lấy ra IQueryable.
        /// </summary>
        /// <value>The raw.</value>
    }
}
