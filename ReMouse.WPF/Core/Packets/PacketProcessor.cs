using ReMouse.Shared.Packets;
using WindowsInput;
using WindowsInput.Native;

namespace ReMouse.WPF.Core.Packets
{
    public class PacketProcessor
    {
        private readonly IKeyboardSimulator _keyboard;
        private readonly IMouseSimulator _mouse;
        private readonly Packet _packet;

        private bool _isDown;

        public PacketProcessor()
        {
            IInputSimulator inputSimulator = new InputSimulator();
            _keyboard = inputSimulator.Keyboard;
            _mouse = inputSimulator.Mouse;

            _packet = new Packet();
        }

        public void ProcessData(byte[] data, int length)
        {
            _packet.SetBytes(data, 0, length);

            switch (_packet.ButtonType)
            {
                case ButtonType.Button_0:
                case ButtonType.Button_1:
                case ButtonType.Button_2:
                case ButtonType.Button_3:
                case ButtonType.Button_4:
                case ButtonType.Button_5:
                case ButtonType.Button_6:
                case ButtonType.Button_7:
                case ButtonType.Button_8:
                case ButtonType.Button_9:
                    ProcessUserButton(_packet);
                    break;
                case ButtonType.Copy:
                    _keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    _keyboard.KeyPress(VirtualKeyCode.VK_C);
                    _keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    break;
                case ButtonType.Paste:
                    _keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    _keyboard.KeyPress(VirtualKeyCode.VK_V);
                    _keyboard.KeyUp(VirtualKeyCode.CONTROL);
                    break;
                case ButtonType.Middle_Click:
                    _mouse.MiddleButtonClick();
                    break;
                case ButtonType.Middle_Down:
                    _mouse.VerticalScroll(-1);
                    break;
                case ButtonType.Middle_Up:
                    _mouse.VerticalScroll(1);
                    break;
                case ButtonType.System_Volume_Mute:
                case ButtonType.System_Volume_Down:
                case ButtonType.System_Volume_Up:
                case ButtonType.Player_Control_Forward:
                case ButtonType.Player_Control_Backward:
                case ButtonType.Player_Control_PlayPause:
                case ButtonType.Player_Control_Volume_Up:
                case ButtonType.Player_Control_Volume_Down:
                    _keyboard.KeyPress((VirtualKeyCode)_packet.ButtonType);
                    break;

                case ButtonType.Right:
                    _mouse.RightButtonClick();
                    break;
                case ButtonType.MoveMouse:
                    _mouse.MoveMouseBy((int)_packet.X, (int)_packet.Y);
                    break;
                case ButtonType.Left:
                    switch (_packet.ButtonBehaviour)
                    {
                        case ButtonBehaviour.CLICK:
                            _mouse.LeftButtonClick();
                            break;
                        case ButtonBehaviour.HOLDORRELEASE:
                            if (_isDown)
                                _mouse.LeftButtonUp();
                            else
                                _mouse.LeftButtonDown();
                            _isDown = !_isDown;
                            break;
                    }
                    break;
                case ButtonType.Keyboard_Input:
                    switch (_packet.ButtonBehaviour)
                    {
                        case ButtonBehaviour.KEY_RETURN:
                        case ButtonBehaviour.KEY_BACK:
                            _keyboard.KeyPress((VirtualKeyCode)_packet.ButtonBehaviour);
                            break;
                        case ButtonBehaviour.KEY_CHAR:
                            _keyboard.TextEntry(_packet.TextInput);
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        private void ProcessUserButton(Packet packet)
        {
            // TODO
        }
    }
}
