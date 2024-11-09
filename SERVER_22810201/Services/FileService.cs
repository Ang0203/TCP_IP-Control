namespace SERVER_22810201
{
    /// <summary>
    /// Lớp dịch vụ để xử lý các thao tác với tệp.
    /// </summary>
    public class FileService : IFileService
    {
        public ResponseObject DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return new ResponseObject
                        (true, $"Xóa tệp {filePath} thành công!");
                }
                else
                {
                    return new ResponseObject
                        (false, $"Tệp {filePath} không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                return new ResponseObject
                        (false, $"Lỗi khi xóa tệp {filePath}: {ex.Message}");
            }
        }
        public ResponseObject CopyFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string base64File = Convert.ToBase64String(File.ReadAllBytes(filePath));
                    return new ResponseObject
                        (true, $"Sao chép tệp {filePath} thành công!", base64File);
                }
                else
                {
                    return new ResponseObject
                        (false, $"Tệp {filePath} không tồn tại");
                }
            }
            catch (Exception ex)
            {
                return new ResponseObject
                        (false, $"Lỗi khi đọc tệp {filePath}: {ex.Message}");
            }
        }
    }
}
