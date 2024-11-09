using System.Net.Sockets;
using System.Windows.Controls;

namespace CLIENT_22810201
{
    public partial class ShutdownPage : Page
    {
        private readonly TcpClient _tcpClient;

        public ShutdownPage(TcpClient tcpClient)
        {
            InitializeComponent();
            _tcpClient = tcpClient;
        }

        // Thực thi lệnh
        private async void ExecuteRequest(RequestObject request)
        {
            try
            {
                await NetworkHelper.SendAndProcessResponseAsync<string>(
                    _tcpClient,
                    request,
                    message => { MessageBox.Show(message); }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tắt hoặc khởi động lại máy: {ex.Message}");
            }
        }
        // Xử lý sự kiện bấm nút Tắt máy
        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị cửa sổ xác nhận
            var result = MessageBox.Show("Bạn có chắc chắn muốn tắt máy?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ExecuteRequest(new RequestObject("SHUTDOWN"));
            }
        }

        // Xử lý sự kiện bấm nút Khởi động lại
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị cửa sổ xác nhận
            var result = MessageBox.Show("Bạn có chắc chắn muốn khởi động lại máy?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ExecuteRequest(new RequestObject("RESTART"));
            }
        }
        // Xử lý sự kiện quay lại
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_tcpClient));
        }
    }
}
