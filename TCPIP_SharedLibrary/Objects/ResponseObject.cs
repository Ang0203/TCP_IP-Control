using System.Text.Json;

namespace TCPIP_SharedLibrary
{
    /// <summary>
    /// Vật thể đại diện cho phản hồi trả về client.
    /// </summary>
    /// <remarks>
    /// Khởi tạo một thể hiện mới của ResponseObject.
    /// </remarks>
    public class ResponseObject(bool success, string message = "", object? data = default)
    {
        /// <summary>
        /// Xác định xem phản hồi có thành công hay không.
        /// </summary>
        public bool Success { get; private set; } = success;
        /// <summary>
        /// Thông điệp mô tả phản hồi.
        /// </summary>
        public string Message { get; private set; } = message;
        /// <summary>
        /// Dữ liệu trả về.
        /// </summary>
        public object Data { get; private set; } = data ?? new object();
        /// <summary>
        /// Kiểu của dữ liệu trả về.
        /// </summary>

        public static T? GetData<T>(string element)
        {
            return JsonSerializer.Deserialize<T>(element);
        }
    }
}
