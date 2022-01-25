using System;
using System.Linq;
using System.Text;

namespace ReMouse.Shared.Packets
{
    public class Packet
    {
        public ButtonType ButtonType { get; set; }
        public ButtonBehaviour ButtonBehaviour { get; set; }

        public float X { get; set; }
        public float Y { get; set; }

        public string TextInput { get; set; }

        public Packet() { }

        public int GetBytes(byte[] buffer, int offset)
        {
            int i = 0;
            Array.Copy(BitConverter.GetBytes((int)ButtonType), 0, buffer, offset + i, sizeof(int));
            i += sizeof(int);
            Array.Copy(BitConverter.GetBytes((int)ButtonBehaviour), 0, buffer, offset + i, sizeof(int));
            i += sizeof(int);

            if (ButtonType == ButtonType.MoveMouse)
            {
                Array.Copy(BitConverter.GetBytes(X), 0, buffer, offset + i, sizeof(float));
                i += sizeof(float);
                Array.Copy(BitConverter.GetBytes(Y), 0, buffer, offset + i, sizeof(float));
                i += sizeof(float);
            }
            else if (ButtonType == ButtonType.Keyboard_Input)
            {
                byte[] b = Encoding.UTF8.GetBytes(TextInput);
                Array.Copy(b, 0, buffer, offset + i, sizeof(float));
                i += b.Length;
            }

            return i;
        }

        public byte[] GetBytes()
        {
            byte[] result = new byte[sizeof(int) + sizeof(int)];

            Array.Copy(BitConverter.GetBytes((int)ButtonType), result, sizeof(int));
            Array.Copy(BitConverter.GetBytes((int)ButtonBehaviour), 0, result, sizeof(int), sizeof(int));

            if (ButtonType == ButtonType.MoveMouse)
            {
                byte[] coord = new byte[sizeof(float) + sizeof(float)];
                Array.Copy(BitConverter.GetBytes(X), 0, coord, 0, sizeof(float));
                Array.Copy(BitConverter.GetBytes(Y), 0, coord, sizeof(float), sizeof(float));
                result = result.Concat(coord).ToArray();
            }
            else if (ButtonType == ButtonType.Keyboard_Input)
            {
                if (TextInput != null && TextInput.Length != 0)
                {
                    byte[] text = Encoding.UTF8.GetBytes(TextInput);
                    result = result.Concat(text).ToArray();
                }
            }

            return result;
        }

        public void SetBytes(byte[] buffer, int offset, int length)
        {
            int i = 0;

            ButtonType = (ButtonType)BitConverter.ToInt32(buffer, offset);
            i += 4;
            ButtonBehaviour = (ButtonBehaviour)BitConverter.ToInt32(buffer, offset + i);
            i += 4;

            if (ButtonType == ButtonType.MoveMouse)
            {
                X = BitConverter.ToSingle(buffer, offset + i);
                i += 4;
                Y = BitConverter.ToSingle(buffer, offset + i);
            }
            else if (ButtonType == ButtonType.Keyboard_Input)
                TextInput = Encoding.UTF8.GetString(buffer, offset + i, length - (offset + i));
        }
    }
}
