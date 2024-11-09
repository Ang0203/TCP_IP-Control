using System.IO;
using System.Net.Sockets;
using System.Windows.Controls;

namespace CLIENT_22810201
{
    public partial class FilePage : Page
    {
        private readonly TcpClient _tcpClient;

        public FilePage(TcpClient tcpClient)
        {
            InitializeComponent();
            _tcpClient = tcpClient;
        }

        // Xử lý yêu cầu gửi đi và nhận phản hồi
        private async void ExecuteRequest<T>(RequestObject request, Action<T?>? handler = null)
        {
            await NetworkHelper.SendAndProcessResponseAsync<T>(
                _tcpClient,
                request,
                message => { MessageBox.Show(message); },
                handler
            );
        }
        // Xử lý sự kiện bấm nút Sao chép tệp
        private void CopyFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy đường dẫn tệp từ ô nhập
                string filePath = FilePathTextBox.Text;
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Vui lòng nhập đường dẫn tệp cần sao chép.");
                    return;
                }

                // Gửi yêu cầu sao chép tệp và nhận base64 từ server
                ExecuteRequest<string>(new RequestObject("COPY_FILE", filePath), base64Response =>
                {
                    if (!string.IsNullOrEmpty(base64Response))
                    {
                        if (string.IsNullOrEmpty(base64Response))
                        {
                            MessageBox.Show("Không nhận được dữ liệu base64 từ server.");
                            return;
                        }

                        // Kiểm tra base64
                        MessageBox.Show("Đã nhận base64 từ server.");
                        MessageBox.Show($"{base64Response}");

                        // Giải mã base64 và lưu thành tệp vào thư mục client
                        string fileName = Path.GetFileName(filePath);
                        SaveBase64ToFile(base64Response, fileName);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sao chép tệp: {ex.Message}");
            }
        }
        // Xử lý sự kiện bấm nút Xóa tệp
        private void DeleteFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy đường dẫn tệp từ ô nhập
                string filePath = FilePathTextBox.Text;
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Vui lòng nhập đường dẫn tệp cần xóa.");
                    return;
                }

                // Gửi yêu cầu xóa tệp
                ExecuteRequest<object>(new RequestObject("DELETE_FILE", filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa tệp: {ex.Message}");
            }
        }
        // Xử lý sự kiện quay lại
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_tcpClient));
        }
        // Phương thức giải mã base64 và lưu tệp vào máy client
        private void SaveBase64ToFile(string base64String, string fileName)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                MessageBox.Show("Dữ liệu base64 không hợp lệ.");
                return;
            }

            try
            {
                // Giải mã base64 thành mảng byte
                byte[] fileBytes = Convert.FromBase64String(base64String);

                // Lưu mảng byte vào tệp với tên file và thư mục hiện tại của ứng dụng
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(currentDirectory, fileName);

                // Lưu mảng byte vào tệp
                File.WriteAllBytes(filePath, fileBytes);

                MessageBox.Show($"Đã lưu tệp tại: {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu tệp: {ex.Message}");
            }
        }

    }
}
