namespace SERVER_22810201
{
    public interface IKeyloggerService
    {
        /// <summary>
        /// Khóa bàn phím.
        /// </summary>
        ResponseObject LockKeyboard();

        /// <summary>
        /// Mở khóa bàn phím.
        /// </summary>
        ResponseObject UnlockKeyboard();
    }
}