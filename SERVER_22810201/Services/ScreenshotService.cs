using System.Drawing.Imaging;

namespace SERVER_22810201
{
    /// <summary>
    /// Lớp dịch vụ chụp màn hình server.
    /// </summary>
    public class ScreenshotService : IScreenshotService
    {
        public ResponseObject CaptureScreenshot()
        {
            try
            {
                // Tạo bitmap mới với kích thước màn hình
                var bounds = Screen.GetBounds(Point.Empty);

                using var bitmap = new Bitmap(bounds.Width, bounds.Height);
                using var graphics = Graphics.FromImage(bitmap);
                using var memoryStream = new MemoryStream();

                // Chụp màn hình server
                graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

                // Lưu bitmap vào MemoryStream
                bitmap.Save(memoryStream, ImageFormat.Jpeg);

                // Chuyển mảng byte thành chuỗi Base64
                string base64Image = Convert.ToBase64String(memoryStream.ToArray());

                // Trả về phản hồi
                return new ResponseObject
                    (true, $"Chụp màn hình thành công!", base64Image);
            }
            catch (Exception ex)
            {
                return new ResponseObject
                    (true, $"Lỗi khi chụp màn hình: {ex.Message}");
            }
        }
    }
}
