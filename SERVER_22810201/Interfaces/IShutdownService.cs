namespace SERVER_22810201
{
    /// <summary>
    /// Giao diện cung cấp các phương thức để tắt và khởi động lại máy.
    /// </summary>
    public interface IShutdownService
    {
        /// <summary>
        /// Tắt máy tính ngay lập tức.
        /// </summary>
        ResponseObject Shutdown();

        /// <summary>
        /// Khởi động lại máy tính ngay lập tức.
        /// </summary>
        ResponseObject Restart();
    }
}