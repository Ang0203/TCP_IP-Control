using System.Diagnostics;

namespace SERVER_22810201
{
    /// <summary>
    /// Lớp dịch vụ quản lý các tiến trình đang chạy trên hệ thống.
    /// </summary>
    public class ProcessProviderService : IProcessProviderService
    {
        private List<ItemInfo> _processes = [];

        public ResponseObject GetItems()
        {
            List<ItemInfo> processes = [];
            try
            {
                // Lấy tất cả các tiến trình đang chạy
                foreach (var process in Process.GetProcesses())
                {
                    processes.Add(new ItemInfo
                    {
                        Name = process.ProcessName,
                        IsRunning = true
                    });
                }

                // Sắp xếp danh sách tiến trình theo tên
                _processes = processes.OrderBy(p => p.Name).ToList();

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Lấy danh sách tiến trình thành công!", _processes);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi lấy danh sách tiến trình: {ex.Message}", _processes);
            }
        }
        public ResponseObject StartItem(string name)
        {
            try
            {
                // Tìm tiến trình theo yêu cầu
                var process = _processes.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (process != null && !process.IsRunning)
                {
                    Process.Start(new ProcessStartInfo(name));
                    process.IsRunning = true;
                }

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Khởi động tiến trình {name} thành công!", _processes);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi khởi động tiến trình tiến trình {name}: {ex.Message}", _processes);
            }
        }
        public ResponseObject StopItem(string name)
        {
            try
            {
                // Tìm tiến trình theo yêu cầu
                var process = _processes.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (process != null && process.IsRunning)
                {
                    var processesToKill = Process.GetProcessesByName(name);
                    foreach (var proc in processesToKill)
                    {
                        proc.Kill();
                    }
                    process.IsRunning = false;
                }

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Dừng tiến trình {name} thành công!", _processes);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                   (false, $"Lỗi khi dừng tiến trình tiến trình {name}: {ex.Message}", _processes);
            }
        }
    }
}
