using ReMouse.Phone.Core.Application;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReMouse.Phone.Droid.DroidCore.ApplicationCloser))]

namespace ReMouse.Phone.Droid.DroidCore
{
    public class ApplicationCloser : IApplicationCloser
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}