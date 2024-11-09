using System.Net.Sockets;
using System.Text.Json;

namespace TCPIP_SharedLibrary
{
    /// <summary>
    /// Lớp tiện ích giúp gửi và nhận dữ liệu qua NetworkStream từ TcpClient.
    /// </summary>
    public static class NetworkStreamHelper
    {
        /// <summary>
        /// Gửi dữ liệu qua Stream của một TcpClient.
        /// </summary>
        /// <param name="client">TcpClient tuyền vào.</param>
        /// <param name="data">Dữ liệu gửi đi.</param>
        /// <returns>Chuỗi thể hiện kết quả.</returns>
        public static async Task<string> SendDataAsync(TcpClient client, object data)
        {
            try
            {
                // Lấy luồng dữ liệu từ client
                NetworkStream stream = client.GetStream();

                // Khởi tạo độ dài một gói tin và số byte đã gửi
                int packetSize = 1024;
                int totalBytesSent = 0;

                // Chuyển dữ liệu thành chuỗi byte
                byte[] dataBytes = JsonSerializer.SerializeToUtf8Bytes(data);

                // Gửi tổng độ dài dữ liệu trước trong 4 byte đầu tiên
                byte[] lengthPrefix = BitConverter.GetBytes(dataBytes.Length);
                await stream.WriteAsync(lengthPrefix);

                // Gửi dữ liệu chia theo từng gói
                while (totalBytesSent < dataBytes.Length)
                {
                    int remainingBytes = dataBytes.Length - totalBytesSent;
                    int bytesToSend = Math.Min(packetSize, remainingBytes);

                    await stream.WriteAsync(dataBytes.AsMemory(totalBytesSent, bytesToSend));
                    totalBytesSent += bytesToSend;
                }

                // Đảm bảo dữ liệu được gửi
                await stream.FlushAsync();

                // Trả về chuỗi trống nếu thành công
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Lỗi khi gửi dữ liệu qua stream: {ex.Message}";
            }
        }
        /// <summary>
        /// Nhận dữ liệu qua Stream của một TcpClient.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu mong đợi.</typeparam>
        /// <param name="client">TcpClient truyền vào.</param>
        /// <returns>Tuple chứa (sting, T) tương ứng kết quả và dữ liệu.</returns>
        public static async Task<(string, T?)> ReceiveDataAsync<T>(TcpClient client)
        {
            try
            {
                // Lấy luồng dữ liệu từ client
                NetworkStream stream = client.GetStream();

                // Khởi tạo độ dài một gói tin và số byte đã nhận
                int packetSize = 1024;
                int totalBytesReceived = 0;

                // Đọc độ dài dữ liệu từ 4 byte đầu tiên
                byte[] lengthPrefix = new byte[4];
                await stream.ReadAsync(lengthPrefix);
                int dataLength = BitConverter.ToInt32(lengthPrefix, 0);

                // Tạo bộ đệm lưu dữ liệu nhận được
                byte[] receivedData = new byte[dataLength];

                // Nhận dữ liệu từng gói
                while (totalBytesReceived < dataLength)
                {
                    int remainingBytes = dataLength - totalBytesReceived;
                    int bytesToRead = Math.Min(packetSize, remainingBytes);

                    int bytesRead = await stream.ReadAsync(receivedData.AsMemory(totalBytesReceived, bytesToRead));
                    if (bytesRead == 0)
                    {
                        throw new IOException("Connection closed unexpectedly.");
                    }

                    totalBytesReceived += bytesRead;
                }

                // Chuyển JSON thành T
                T? result = JsonSerializer.Deserialize<T>(receivedData);
                if (result == null)
                {
                    return ($"Kiểu dữ liệu nhận được không hợp lệ.", default);

                }

                // Trả về kết quả
                return (string.Empty, result);
            }
            catch (Exception ex)
            {
                return ($"Lỗi khi đọc dữ liệu qua stream: {ex.Message}", default);
            }
        }
    }
}