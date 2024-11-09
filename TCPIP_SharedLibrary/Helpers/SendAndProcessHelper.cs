using System.Net.Sockets;
using System.Text.Json;

namespace TCPIP_SharedLibrary
{
    public static class NetworkHelper
    {
        /// <summary>
        /// Gửi yêu cầu qua TcpClient và xử lý phản hồi bằng một delegate tùy chỉnh.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu mong đợi từ response.Data.</typeparam>
        /// <param name="tcpClient">TcpClient để gửi/nhận dữ liệu.</param>
        /// <param name="request">Đối tượng yêu cầu.</param>
        /// <param name="msgBox">Hàm để hiển thị thông báo lỗi (ví dụ: MessageBox.Show).</param>
        /// <param name="handleResponse">Delegate để xử lý kết quả phản hồi thành công.</param>
        public static async Task SendAndProcessResponseAsync<T>(
            TcpClient tcpClient,
            RequestObject request,
            Action<string>? msgBox = null,
            Action<T?>? handleResponse = null)
        {
            try
            {
                // Gửi yêu cầu
                string sendResult = await NetworkStreamHelper.SendDataAsync(tcpClient, request);
                if (!string.IsNullOrEmpty(sendResult))
                {
                    msgBox?.Invoke(sendResult);
                    return;
                }

                // Nhận phản hồi
                (string receiveResult, ResponseObject? response) = await NetworkStreamHelper.ReceiveDataAsync<ResponseObject>(tcpClient);
                if (!string.IsNullOrEmpty(receiveResult) || response == null)
                {
                    msgBox?.Invoke(receiveResult);
                    return;
                }

                // Kiểm tra thành công
                if (!response.Success)
                {
                    msgBox?.Invoke(response.Message);
                    return;
                }

                // Nếu thành công và có thông báo
                if (!string.IsNullOrEmpty(response.Message))
                {
                    msgBox?.Invoke(response.Message);
                }

                // Chuyển đổi dữ liệu và sử dụng handleResponse nếu có
                if (response.Data is JsonElement jsonElement)
                {
                    try
                    {
                        var data = ResponseObject.GetData<T>(jsonElement.GetRawText());
                        handleResponse?.Invoke(data);
                    }
                    catch (Exception ex)
                    {
                        msgBox?.Invoke($"[CLIENT] Lỗi khi chuyển đổi dữ liệu: {ex.Message}");
                    }
                }
                else
                {
                    // Lỗi nếu không nhận được dữ liệu hợp lệ
                    msgBox?.Invoke("[CLIENT] Kiểu dữ liệu trả về không hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                msgBox?.Invoke($"[CLIENT] Lỗi khi xử lý yêu cầu: {ex.Message}");
            }
        }
    }
}
