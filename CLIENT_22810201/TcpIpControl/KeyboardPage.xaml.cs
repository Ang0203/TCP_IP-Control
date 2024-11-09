using System.Net.Sockets;
using System.Windows.Controls;

namespace CLIENT_22810201
{
    public partial class KeyboardPage : Page
    {
        private readonly TcpClient _tcpClient;

        public KeyboardPage(TcpClient tcpClient)
        {
            InitializeComponent();
            _tcpClient = tcpClient;
        }

        // Xử lý yêu cầu bàn phím
        private async void ExecuteRequest(RequestObject request)
        {
            await NetworkHelper.SendAndProcessResponseAsync<object>(
                _tcpClient,
                request,
                message => { MessageBox.Show(message); }
            );
        }
        // Xử lý sự kiện bấm nút Khóa bàn phím
        private void LockKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gửi yêu cầu khóa bàn phím
                ExecuteRequest(new RequestObject("LOCK_KEYBOARD"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khóa bàn phím: {ex.Message}");
            }
        }
        // Xử lý sự kiện bấm nút Mở khóa bàn phím
        private void UnlockKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gửi yêu cầu mở khóa bàn phím
                ExecuteRequest(new RequestObject("UNLOCK_KEYBOARD"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở khóa bàn phím: {ex.Message}");
            }
        }
        // Xử lý sự kiện quay lại
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_tcpClient));
        }
    }
}
