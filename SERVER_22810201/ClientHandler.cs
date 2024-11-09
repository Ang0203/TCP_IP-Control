using System.Net.Sockets;

namespace SERVER_22810201
{
    /// <summary>
    /// Xử lý các yêu cầu từ client kết nối đến server.
    /// </summary>
    public class ClientHandler
    {
        private readonly IApplicationProviderService _applicationProvider;
        private readonly IProcessProviderService _processProvider;
        private readonly IShutdownService _shutdownService;
        private readonly IScreenshotService _screenshotService;
        private readonly IKeyloggerService _keyloggerService;
        private readonly IFileService _fileService;
        private readonly Dictionary<string, Func<TcpClient, string?, ResponseObject>> _requestHandlers;

        public ClientHandler(
            IApplicationProviderService applicationProvider,
            IProcessProviderService processProvider,
            IShutdownService shutdownService,
            IScreenshotService screenshotService,
            IKeyloggerService keyloggerService,
            IFileService fileService)
        {
            _applicationProvider = applicationProvider;
            _processProvider = processProvider;
            _shutdownService = shutdownService;
            _screenshotService = screenshotService;
            _keyloggerService = keyloggerService;
            _fileService = fileService;

            _requestHandlers = new Dictionary<string, Func<TcpClient, string?, ResponseObject>>
            {
                { "GET_APPS", HandleGetApps },
                { "START_APP", HandleStartApp },
                { "STOP_APP", HandleStopApp },
                { "GET_PROCS", HandleGetProcs },
                { "START_PROC", HandleStartProc },
                { "STOP_PROC", HandleStopProc },
                { "SHUTDOWN", HandleShutdown },
                { "RESTART", HandleRestart },
                { "SCREENSHOT", HandleCaptureScreenshot },
                { "LOCK_KEYBOARD", HandleLockKeyboard },
                { "UNLOCK_KEYBOARD", HandleUnlockKeyboard },
                { "COPY_FILE", HandleCopyFile },
                { "DELETE_FILE", HandleDeleteFile }
            };
        }

        /// <summary>
        /// Xử lý yêu cầu lấy danh sách ứng dụng.
        /// </summary>
        private ResponseObject HandleGetApps(TcpClient client, string? _)
        {
            return _applicationProvider.GetItems();
        }
        /// <summary>
        /// Xử lý yêu cầu khởi động ứng dụng.
        /// </summary>
        private ResponseObject HandleStartApp(TcpClient client, string? appName)
        {
            return _applicationProvider.StartItem(appName ?? string.Empty);
        }
        /// <summary>
        /// Xử lý yêu cầu dừng ứng dụng.
        /// </summary>
        private ResponseObject HandleStopApp(TcpClient client, string? appName)
        {
            return _applicationProvider.StopItem(appName ?? string.Empty);
        }
        /// <summary>
        /// Xử lý yêu cầu lấy danh sách tiến trình đang chạy.
        /// </summary>
        private ResponseObject HandleGetProcs(TcpClient client, string? _)
        {
            return _processProvider.GetItems();
        }
        /// <summary>
        /// Xử lý yêu cầu khởi động tiến trình.
        /// </summary>
        private ResponseObject HandleStartProc(TcpClient client, string? procName)
        {
            return _processProvider.StartItem(procName ?? string.Empty);
        }
        /// <summary>
        /// Xử lý yêu cầu dừng tiến trình.
        /// </summary>
        private ResponseObject HandleStopProc(TcpClient client, string? procName)
        {
            return _processProvider.StopItem(procName ?? string.Empty);
        }
        /// <summary>
        /// Xử lý yêu cầu tắt máy chủ.
        /// </summary>
        private ResponseObject HandleShutdown(TcpClient client, string? _)
        {
            return _shutdownService.Shutdown();
        }
        /// <summary>
        /// Xử lý yêu cầu khởi động lại máy chủ.
        /// </summary>
        private ResponseObject HandleRestart(TcpClient client, string? _)
        {
            return _shutdownService.Restart();
        }
        /// <summary>
        /// Xử lý yêu cầu chụp màn hình.
        /// </summary>
        private ResponseObject HandleCaptureScreenshot(TcpClient client, string? _)
        {
            return _screenshotService.CaptureScreenshot();
        }
        /// <summary>
        /// Xử lý yêu cầu khóa bàn phím.
        /// </summary>
        private ResponseObject HandleLockKeyboard(TcpClient client, string? _)
        {
            return _keyloggerService.LockKeyboard();
        }
        /// <summary>
        /// Xử lý yêu cầu mở khóa bàn phím.
        /// </summary>
        private ResponseObject HandleUnlockKeyboard(TcpClient client, string? _)
        {
            return _keyloggerService.UnlockKeyboard();
        }
        /// <summary>
        /// Xử lý yêu cầu xóa tệp.
        /// </summary>
        private ResponseObject HandleDeleteFile(TcpClient client, string? filePath)
        {
            return _fileService.DeleteFile(filePath ?? string.Empty);
        }
        /// <summary>
        /// Xử lý yêu cầu sao chép tệp.
        /// </summary>
        private ResponseObject HandleCopyFile(TcpClient client, string? filePath)
        {
            return _fileService.CopyFile(filePath ?? string.Empty);
        }

        /// <summary>
        /// Xử lý kết nối của client và nhận yêu cầu từ client.
        /// </summary>
        public async Task HandleClient(TcpClient client)
        {
            try
            {
                // Lấy luồng dữ liệu từ client
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    // Xử lý yêu cầu cho đến khi mất kết nối
                    if (!client.Connected || !stream.CanRead)
                    {
                        MessageBox.Show("Kết nối đã bị ngắt hoặc stream không thể đọc.");
                        break;
                    }

                    // Nhận yêu cầu
                    (string result, RequestObject? request) = await NetworkStreamHelper.ReceiveDataAsync<RequestObject>(client);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show(result);
                        return;
                    }

                    // Xử lý yêu cầu
                    string result2 = string.Empty;
                    if (_requestHandlers.TryGetValue(request!.Command.ToUpper(), out var handler))
                    {
                        ResponseObject response = handler(client, request.Payload != "" ? request.Payload : null);
                        result2 = await NetworkStreamHelper.SendDataAsync(client, response);
                    }
                    else
                    {
                        ResponseObject errorResponse = new(false, $"Lỗi: Yêu cầu không tồn tại.");
                        result2 = await NetworkStreamHelper.SendDataAsync(client, errorResponse);
                    }

                    // Thông báo nếu result không rỗng (lỗi)
                    if (!string.IsNullOrEmpty(result2)) MessageBox.Show(result2);
                }
            }
            catch (Exception ex)
            {
                ResponseObject errorResponse = new(false, $"[SERVER] Lỗi khi xử lý yêu cầu từ client: {ex.Message}");
                string result = await NetworkStreamHelper.SendDataAsync(client, errorResponse);

                // Thông báo nếu result không rỗng (lỗi)
                if (!string.IsNullOrEmpty(result)) MessageBox.Show(result);
            }
        }
    }
}
