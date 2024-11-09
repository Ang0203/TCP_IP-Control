namespace TCPIP_SharedLibrary
{
    /// <summary>
    /// Vật thể đại diện cho thông tin của một mục (ứng dụng hoặc tiến trình).
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// Tên của một mục (ứng dụng hoặc tiến trình).
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Trạng thái chạy của một mục (ứng dụng hoặc tiến trình).
        /// </summary>
        public bool IsRunning { get; set; }
    }
}
