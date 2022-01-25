using ReMouse.Phone.Core.RepositoryData;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public abstract class ButtonFactory
    {
        protected readonly Queue<IButton> buttons = new Queue<IButton>();
        protected readonly RelativeLayout layout;
        protected readonly ButtonRepository buttonRepository;

        public ButtonFactory(RelativeLayout layout)
        {
            this.layout = layout;
            buttonRepository = DependencyService.Get<ButtonRepository>();
        }

        public abstract void CreateButtons();

        public void DestroyButtons()
        {
            while (buttons.Count != 0)
                buttons.Dequeue().Destroy();
        }

        public void Save()
        {
            buttonRepository.Save();
        }
    }
}
