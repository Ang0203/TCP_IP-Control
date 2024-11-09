namespace SERVER_22810201
{
    /// <summary>
    /// Giao diện cung cấp các phương thức để quản lý tệp tin.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Xóa một tệp.
        /// </summary>
        ResponseObject DeleteFile(string filePath);

        /// <summary>
        /// Lấy nội dung của tệp từ máy chủ và gửi về client.
        /// </summary>
        ResponseObject CopyFile(string filePath);
    }
}