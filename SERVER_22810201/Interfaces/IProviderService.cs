namespace SERVER_22810201
{
    /// <summary>
    /// Giao diện cung cấp các phương thức để quản lý các mục (ứng dụng hoặc tiến trình).
    /// </summary>
    public interface IProviderService
    {
        /// <summary>
        /// Lấy danh sách các mục.
        /// </summary>
        ResponseObject GetItems();

        /// <summary>
        /// Khởi động một mục dựa trên tên.
        /// </summary>
        ResponseObject StartItem(string name);

        /// <summary>
        /// Dừng một mục dựa trên tên.
        /// </summary>
        ResponseObject StopItem(string name);
    }
    /// <summary>
    /// Giao diện cung cấp các phương thức để quản lý các ứng dụng.
    /// </summary>
    public interface IApplicationProviderService : IProviderService { }
    /// <summary>
    /// Giao diện cung cấp các phương thức để quản lý các tiến trình.
    /// </summary>
    public interface IProcessProviderService : IProviderService { }
}