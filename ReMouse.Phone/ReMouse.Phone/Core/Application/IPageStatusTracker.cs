
namespace ReMouse.Phone.Core.Application
{
    public interface IPageStatusTracker
    {
        void OnPagePushed();
        void OnPagePopped();
    }
}
