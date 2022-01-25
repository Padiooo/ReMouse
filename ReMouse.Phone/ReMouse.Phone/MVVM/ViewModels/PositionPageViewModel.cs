using ReMouse.Phone.Core.Application;
using ReMouse.Phone.Core.Mouse;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class PositionPageViewModel : IPageStatusTracker
    {
        private readonly List<IButton> buttons = new List<IButton>();
        private readonly DraggableButtonFactory factory;

        public PositionPageViewModel(RelativeLayout layout)
        {
            factory = new DraggableButtonFactory(layout);
        }

        public void OnPagePushed()
        {
            factory.CreateButtons();
        }

        public void OnPagePopped()
        {
            factory.DestroyButtons();
            factory.Save();
        }
    }
}
