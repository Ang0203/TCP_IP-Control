using System.Diagnostics;
using IWshRuntimeLibrary;

namespace SERVER_22810201
{
    /// <summary>
    /// Lớp dịch vụ quản lý các ứng dụng đã cài đặt.
    /// </summary>
    public class ApplicationProviderService : IApplicationProviderService
    {
        private List<ItemInfo> _applications = [];
        private readonly string[] _directories =
        {
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu),
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)
        };

        /// <summary>
        /// Tìm tệp thực thi từ tệp shortcut.
        /// </summary>
        /// <param name="appName">Tên của ứng dụng cần tìm đường dẫn.</param>
        /// <returns>Đường dẫn tệp thực thi nếu tìm thấy, ngược lại null.</returns>
        private string? FindApplicationPath(string appName)
        {
            foreach (var directory in _directories)
            {
                var lnkFiles = Directory.GetFiles(directory, "*.lnk", SearchOption.AllDirectories);
                foreach (var lnkFile in lnkFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(lnkFile);
                    if (fileName.Equals(appName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Lấy đường dẫn thực của tệp thực thi từ shortcut .lnk
                        var exePath = GetTargetPathFromLnk(lnkFile);
                        if (!string.IsNullOrEmpty(exePath))
                        {
                            return exePath;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Đọc đường dẫn tệp shortcut trỏ tới.
        /// </summary>
        /// <param name="lnkPath">Đường dẫn tới tệp .lnk.</param>
        /// <returns>Đường dẫn tệp thực thi mà shortcut trỏ tới.</returns>
        private string? GetTargetPathFromLnk(string lnkPath)
        {
            try
            {
                WshShell shell = new();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(lnkPath);
                return shortcut.TargetPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy đường dẫn từ file .lnk: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Kiểm tra xem ứng dụng có đang chạy hay không.
        /// </summary>
        /// <param name="appName">Tên của ứng dụng cần kiểm tra.</param>
        /// <returns>Trả về true nếu ứng dụng đang chạy, ngược lại false.</returns>
        private static bool IsApplicationRunning(string appName)
        {
            var processes = Process.GetProcessesByName(appName);
            return processes.Length > 0;
        }

        public ResponseObject GetItems()
        {
            // Đặt lại danh sách và tạo danh sách tạm
            _applications.Clear();
            List<ItemInfo> applications = new List<ItemInfo>();

            try
            {
                foreach (var directory in _directories)
                {
                    // Lấy các tệp shortcut
                    var lnkFiles = Directory.GetFiles(directory, "*.lnk", SearchOption.AllDirectories);
                    foreach (var lnkFile in lnkFiles)
                    {
                        // Lấy tên từ các tệp
                        var appName = Path.GetFileNameWithoutExtension(lnkFile);

                        // Kiểm tra ứng dụng đang chạy
                        bool isRunning = IsApplicationRunning(appName);

                        // Thêm vào danh sách
                        applications.Add(new ItemInfo { Name = appName, IsRunning = isRunning });
                    }
                }

                // Sắp xếp theo tên
                _applications = applications.OrderBy(app => app.Name).ToList();

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Lấy danh sách ứng dụng thành công!", _applications);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi lấy danh sách ứng dụng: {ex.Message}", _applications);
            }
        }
        public ResponseObject StartItem(string appName)
        {
            try
            {
                // Tìm ứng dụng theo yêu cầu
                var app = _applications.FirstOrDefault(a => a.Name.Equals(appName, StringComparison.OrdinalIgnoreCase));
                if (app != null && !app.IsRunning)
                {
                    // Tìm đường dẫn theo tên
                    var appPath = FindApplicationPath(appName);

                    // Khởi động ứng dụng
                    if (!string.IsNullOrEmpty(appPath))
                    {
                        Process.Start(appPath);
                        app.IsRunning = true;
                    }
                }

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Khởi động ứng dụng {appName} thành công!", _applications);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi khởi động ứng dụng {appName}: {ex.Message}", _applications);
            }
        }
        public ResponseObject StopItem(string appName)
        {
            try
            {
                // Tìm ứng dụng theo yêu cầu
                var app = _applications.FirstOrDefault(a => a.Name.Equals(appName, StringComparison.OrdinalIgnoreCase));
                if (app != null && app.IsRunning)
                {
                    // Tìm tất cả tiến trình và lọc các tiến trình có tên chứa chuỗi appName
                    var processes = Process.GetProcesses()
                        .Where(p => p.ProcessName.Contains(appName, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    // Dừng ứng dụng
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                    app.IsRunning = false;
                }

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Dừng ứng dụng {appName} thành công!", _applications);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi dừng ứng dụng {appName}: {ex.Message}", _applications);
            }
        }
    }
}
