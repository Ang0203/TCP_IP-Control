using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;

namespace SERVER_22810201
{
    public partial class SERVER_22810201 : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ClientHandler _clientHandler;
        private TcpListener _listener;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isServerRunning;

        public SERVER_22810201(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _clientHandler = serviceProvider.GetService<ClientHandler>();

            InitializeComponent();

            // Gán chức năng cho WinForm
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += SERVER_22810201_FormClosing;
            this.Resize += SERVER_22810201_Resize;

            // Gán chức năng trên khay hệ thống
            NotifyIcon1.MouseClick += NotifyIcon1_MouseClick;
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;

            // Gán chức năng cho nhập liệu IP và Port
            TxtIpAddress.KeyDown += TxtInput_KeyDown;
            TxtPort.KeyDown += TxtInput_KeyDown;
            BtnConfirm.Click += BtnConfirm_Click;

            // Khởi chạy máy chủ
            InitializeServer();
        }

        /// <summary>
        /// Khởi tạo máy chủ với một TcpListener.
        /// </summary>
        private void InitializeServer(string ip = "192.168.109.128", int port = 8080)
        {
            try
            {
                // Đặt lại cancellation token
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                // Đặt lại listener
                _listener?.Stop();
                _listener = new TcpListener(IPAddress.Parse(ip), port);
                _listener.Start();

                // Đặt trạng thái máy chủ
                LblCurrentIpPort.Text = $"Địa chỉ hiện tại: {ip}:{port}";
                UpdateStatus(false, "Đang kết nối...", Color.Goldenrod);

                // Lắng nghe kết nối
                Task.Run(() => ListenForClientsAsync(_cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thiết lập máy chủ: {ex.Message}");
            }
        }
        /// <summary>
        /// Tác vụ async lắng nghe kết nối tcp.
        /// </summary>
        private async Task ListenForClientsAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_listener.Pending())
                    {
                        // Chấp nhận kết nối
                        TcpClient client = _listener.AcceptTcpClient();

                        // Đặt trạng thái máy chủ
                        UpdateStatus(true, "Đã kết nối!", Color.Green);

                        // Xử lý client trong background
                        await _clientHandler.HandleClient(client);

                        // Đóng kết nối
                        client.Close();
                        UpdateStatus(false, "Đang kết nối...", Color.Goldenrod);
                    }
                    else
                    {
                        await Task.Delay(500, cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lắng nghe kết nối: {ex.Message}");
            }
        }

        #region Chức năng WinForm
        /// <summary>
        /// Thu nhỏ xuống system tray khi thu nhỏ ứng dụng.
        /// </summary>
        private void SERVER_22810201_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                NotifyIcon1.Visible = true;
            }
        }

        /// <summary>
        /// Sự kiện khi đóng form.
        /// </summary>
        private void SERVER_22810201_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hủy bỏ các tác vụ và dừng listener khi đóng cửa sổ ứng dụng
            _cancellationTokenSource?.Cancel();
            _listener?.Stop();
        }
        #endregion

        #region Chức năng trên khay hệ thống
        /// <summary>
        /// Trên icon system tray:
        /// - Chuột trái hiện lại cửa sổ ứng dụng.
        /// - Chuột phải mở các tùy chọn.
        /// </summary>
        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                NotifyIcon1.Visible = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip1.Show(Cursor.Position);
            }
        }

        /// <summary>
        /// Tùy chọn trong icon system tray, thoát ứng dụng.
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Chức năng nhập liệu IP và Port
        /// <summary>
        /// Quản lý trạng thái máy chủ.
        /// </summary>
        private void UpdateStatus(bool running, string text, Color color)
        {
            _isServerRunning = running;
            ToggleInputFields(!running);
            LblStatus.Text = text;
            LblStatus.ForeColor = color;
        }

        /// <summary>
        /// Quản lý việc nhập liệu tùy vào trạng thái kết nối.
        /// </summary>
        private void ToggleInputFields(bool enable)
        {
            TxtIpAddress.Enabled = enable;
            TxtPort.Enabled = enable;
            BtnConfirm.Enabled = enable;
        }

        /// <summary>
        /// Nhấn Enter để submit.
        /// </summary>
        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !_isServerRunning)
            {
                BtnConfirm.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Nhấn nút "Confirm" để thay đổi IP và port.
        /// </summary>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            string newIp = TxtIpAddress.Text;
            int newPort;

            if (IPAddress.TryParse(newIp, out var ip) && int.TryParse(TxtPort.Text, out newPort) && newPort > 1024 && newPort < 65535)
            {
                InitializeServer(newIp, newPort);
            }
            else
            {
                MessageBox.Show("IP hay Port không hợp lệ. Vui lòng thử lại.\nPort từ 1025 - 65564");
            }
        }
        #endregion
    }
}
