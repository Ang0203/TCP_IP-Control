namespace SERVER_22810201
{
    /// <summary>
    /// Giao diện cung cấp các phương thức để chụp màn hình.
    /// </summary>
    public interface IScreenshotService
    {
        /// <summary>
        /// Chụp màn hình và lưu vào mảng byte.
        /// </summary>
        ResponseObject CaptureScreenshot();
    }
}