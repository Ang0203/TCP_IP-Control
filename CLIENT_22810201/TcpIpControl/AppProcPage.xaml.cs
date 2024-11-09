using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Controls;
using System.Windows.Data;

namespace CLIENT_22810201
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Đang chạy" : "Tạm dừng";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BooleanToNegatedBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class AppProcPage : Page
    {
        private readonly TcpClient _tcpClient;
        private List<ItemInfo>? _applications = [];

        public string PageTitle { get; set; }

        public AppProcPage(TcpClient tcpClient, string pageTitle)
        {
            InitializeComponent();

            _tcpClient = tcpClient;

            PageTitle = pageTitle;
            DataContext = this;

            LoadApplications();
        }

        // Cập nhật giao diện
        private async void UpdateView(RequestObject request)
        {
            await NetworkHelper.SendAndProcessResponseAsync<List<ItemInfo>>(
                _tcpClient,
                request,
                message => { MessageBox.Show(message); },
                handleResponse: items => { ApplicationsDataGrid.ItemsSource = items; _applications = items; }
            );
        }
        // Lấy danh sách
        private void LoadApplications()
        {
            string action = PageTitle == "Ứng dụng" ? "GET_APPS" : "GET_PROCS";
            UpdateView(new RequestObject(action));
        }
        // Bấm nút
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ItemInfo selectedApp)
            {
                string action = (selectedApp.IsRunning, PageTitle) switch
                {
                    (true, "Ứng dụng") => "STOP_APP",
                    (false, "Ứng dụng") => "START_APP",
                    (true, _) => "STOP_PROC",
                    _ => "START_PROC"
                };

                UpdateView(new RequestObject(action, selectedApp.Name));
            }
        }
        // Sự kiện cho ô tìm kiếm để lọc danh sách
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = SearchTextBox.Text?.ToLower() ?? string.Empty;
            var filteredApplications = _applications?
                .Where(app => app.Name.ToLower().Contains(filter))
                .ToList();

            ApplicationsDataGrid.ItemsSource = filteredApplications;
        }
        // Nút quay lại
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_tcpClient));
        }
    }
}
