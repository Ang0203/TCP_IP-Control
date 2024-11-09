using System.Net.Sockets;

namespace CLIENT_22810201
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = txtServerIP.Text;
            int port;

            // Kiểm tra port
            if (!int.TryParse(txtPort.Text, out port))
            {
                txtStatus.Text = "Port không hợp lệ!";
                txtStatus.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                TcpClient client = new TcpClient(serverIp, port);

                // Kiểm tra kết nối
                if (client.Connected)
                {
                    ConnectionPanel.Visibility = Visibility.Collapsed;
                    MainFrame.Visibility = Visibility.Visible;

                    MainFrame.Navigate(new MainPage(client));
                }
                else
                {
                    txtStatus.Text = $"Kết nối thất bại, xin thử lại.";
                    txtStatus.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"Kết nối thất bại: {ex.Message}";
                txtStatus.Visibility = Visibility.Visible;
            }
        }
    }
}