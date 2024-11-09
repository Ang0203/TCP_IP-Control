using System.Net.Sockets;
using System.Windows.Controls;

namespace CLIENT_22810201
{
    public partial class MainPage : Page
    {
        private readonly TcpClient _tcpClient;

        public MainPage(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;

            InitializeComponent();
        }

        private void BtnApps_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppProcPage(_tcpClient, "Ứng dụng"));
        }
        private void BtnProcs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppProcPage(_tcpClient, "Tiến trình"));
        }
        private void BtnPC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShutdownPage(_tcpClient));
        }
        private void BtnScreen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ScreenshotPage(_tcpClient));
        }
        private void BtnKey_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KeyboardPage(_tcpClient));
        }
        private void BtnFiles_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FilePage(_tcpClient));
        }
    }
}
