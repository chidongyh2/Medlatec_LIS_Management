namespace LIS.Core.Domain.Constants
{
    /// <summary>
    /// Loại Role. Bao gồm 2 kiểu 
    /// 0: Hệ thông. (Không có phép sửa hoặc xóa).
    /// 1: Tùy chỉnh từ người dùng.
    /// </summary>
    public enum RoleType
    {
        BuiltIn,
        Custom
    }
}
