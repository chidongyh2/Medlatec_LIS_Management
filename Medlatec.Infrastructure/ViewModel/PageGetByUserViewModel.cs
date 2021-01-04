namespace Medlatec.Infrastructure.ViewModel
{
    public class PageGetByUserViewModel
    {
        /// <summary>
        /// Mã Trang tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên trang.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Đường dẫn Id của trang.
        /// </summary>
        public string IdPath { get; set; }

        /// <summary>
        /// Đường dẫn trang.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Icon của trang.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Mầy nền của trang.
        /// </summary>

        /// <summary>
        /// Thứ tự hiển thị của trang.
        /// </summary>
        public int Order { get; set; }

        public string OrderPath { get; set; }

        /// <summary>
        /// Mã trang cha.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Số lượng trang con.
        /// </summary>
        public int ChildCount { get; set; }

       /// <summary>
       /// Loại trang
       /// </summary>
        public int Type { get; set; }
    }
}
