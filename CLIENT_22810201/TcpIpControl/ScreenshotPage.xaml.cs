using System.IO;
using System.Net.Sockets;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CLIENT_22810201
{
    public partial class ScreenshotPage : Page
    {
        private readonly TcpClient _tcpClient;

        public ScreenshotPage(TcpClient tcpClient)
        {
            InitializeComponent();
            _tcpClient = tcpClient;
        }

        // Xử lý sự kiện bấm nút Chụp màn hình
        private async void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gửi yêu cầu chụp màn hình
                await NetworkHelper.SendAndProcessResponseAsync<string>(
                    _tcpClient,
                    new RequestObject("SCREENSHOT"),
                    message => { MessageBox.Show(message); },
                    handleResponse: DisplayCapturedImage
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chụp màn hình: {ex.Message}");
            }
        }

        // Xử lý sự kiện bấm nút Quay lại
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_tcpClient));
        }

        // Hiển thị ảnh từ chuỗi Base64 lên Image control
        private void DisplayCapturedImage(string? base64Image)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                MessageBox.Show("Không có hình ảnh để hiển thị.");
                return;
            }

            // Tiếp tục xử lý base64Image nếu không null
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using var ms = new MemoryStream(imageBytes);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            CapturedImage.Source = bitmap;
        }
    }
}
