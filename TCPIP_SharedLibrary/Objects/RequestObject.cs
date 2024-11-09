namespace TCPIP_SharedLibrary
{
    /// <summary>
    /// Vật thể đại diện cho phản hồi trả về client.
    /// </summary>
    public class RequestObject
    {
        /// <summary>
        /// Mã lệnh yêu cầu.
        /// </summary>
        public string Command { get; private set; }
        /// <summary>
        /// Thông tin thêm.
        /// </summary>
        public string Payload { get; private set; }

        /// <summary>
        /// Khởi tạo một thể hiện mới của ResponseObject.
        /// </summary>
        public RequestObject(string command, string payload = "")
        {
            Command = command;
            Payload = payload;
        }
    }
}
