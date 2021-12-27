
namespace ReMouse.WPF.Core.CommandLine
{
    public enum ConnectionModeOption
    {
        NONE,
        BLUETOOTH,
        WIFI
    }

    public class ConnectionModeArgument : ModeArgument<ConnectionModeOption>
    {
        public ConnectionModeArgument(string[] args)
            : base(args, new string[] { "-connectionmode", "-cm", "-connection" })
        {

        }
    }

    public enum WindowModeOption
    {
        DEFAULT,
        MIN,
        HIDDEN
    }

    public class WindowModeArgument : ModeArgument<WindowModeOption>
    {
        public WindowModeArgument(string[] args)
            : base(args, new string[] { "-windowmode", "-wm", "-window" })
        {

        }
    }
}
