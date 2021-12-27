namespace ReMouse.Shared.Packets
{
    public enum ButtonBehaviour
    {
        DEFAULT,

        CLICK,
        HOLDORRELEASE,

        KEY_RETURN = 13,        // Erase
        KEY_BACK = 8,           // Enter
        KEY_CHAR,               // char sent
    }
}
