using System.Diagnostics;

namespace SERVER_22810201
{
    /// <summary>
    /// Lớp dịch vụ quản lý các lệnh tắt và khởi động lại máy tính.
    /// </summary>
    public class ShutdownService : IShutdownService
    {
        public ResponseObject Shutdown()
        {
            try
            {
                Process.Start(new ProcessStartInfo("shutdown", "/s /t 0") { CreateNoWindow = true });
                return new ResponseObject
                    (true, $"Tắt máy thành công!");
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi tắt máy: {ex.Message}");
            }
        }
        public ResponseObject Restart()
        {
            try
            {
                Process.Start(new ProcessStartInfo("shutdown", "/r /t 0") { CreateNoWindow = true });
                return new ResponseObject
                    (true, $"Khởi động lại máy thành công!");
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi khởi động lại máy: {ex.Message}");
            }
        }
    }
}
