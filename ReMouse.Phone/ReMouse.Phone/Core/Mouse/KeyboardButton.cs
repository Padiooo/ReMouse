using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Shared.Packets;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class KeyboardButton : ButtonBehaviourBase
    {
        private readonly Packet packet;
        private readonly Entry entry;
        private readonly ImageSource unknown;

        private readonly byte[] erase;
        private readonly byte[] enter;

        private readonly Dictionary<string, ImageSource> textToImageSource = new Dictionary<string, ImageSource>();

        public KeyboardButton(RelativeLayout layout, ButtonData buttonData) : base(layout, buttonData)
        {
            unknown = ImageSource.FromFile("erase.png");

            char a = 'a';
            for (int i = 0; i < 26; i++)
            {
                char c = (char)(a + i);
                textToImageSource.Add(c.ToString(), ImageSource.FromFile(c + ".png"));
            }

            packet = new Packet
            {
                ButtonType = ButtonType.Keyboard_Input,
            };

            packet.ButtonBehaviour = ButtonBehaviour.KEY_RETURN;
            enter = packet.GetBytes();

            packet.ButtonBehaviour = ButtonBehaviour.KEY_BACK;
            erase = packet.GetBytes();

            entry = new Entry
            {
                Keyboard = Keyboard.Default,
                Text = " "
            };
            layout.Children.Add(entry, () => new Rectangle(-100, -100, 20, 20));
            entry.TextChanged += Entry_TextChanged;
        }

        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            entry.TextChanged -= Entry_TextChanged;

            byte[] buffer;

            ImageSource source;
            // Erase
            if (e.NewTextValue.Length == 0)
            {
                buffer = erase;
                source = unknown;
            }
            // Text
            else
            {
                packet.TextInput = e.NewTextValue.Remove(0, 1);

                if (!textToImageSource.TryGetValue(packet.TextInput.ToLower(), out source))
                    source = unknown;

                packet.ButtonBehaviour = ButtonBehaviour.KEY_CHAR;
                buffer = packet.GetBytes();
            }

            await SendAsync(buffer, 0, buffer.Length);

            image.Source = source;

            entry.Text = " ";

            entry.TextChanged += Entry_TextChanged;
        }

        protected override Task OnTap()
        {
            entry.Focus();
            return base.OnTap();
        }

        protected override async Task OnLongTap() => await SendAsync(enter, 0, enter.Length).ConfigureAwait(false);
    }
}
