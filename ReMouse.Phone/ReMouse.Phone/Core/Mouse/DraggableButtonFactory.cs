using Xamarin.Forms;

namespace ReMouse.Phone.Core.Mouse
{
    public class DraggableButtonFactory : ButtonFactory
    {
        public DraggableButtonFactory(RelativeLayout layout) : base(layout) { }

        public override void CreateButtons()
        {
            foreach (var data in buttonRepository.GetAll())
                if (data.IsEnabled)
                    buttons.Enqueue(new ButtonDraggable(layout, data));
        }
    }
}
