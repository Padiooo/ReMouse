using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Helpers
{
    public static class NavigationExtensions
    {
        public static async Task PushAsync(this Page page, INavigation navigation)
        {
            if (page.Parent == null)
                await navigation.PushAsync(page);
        }
    }
}
