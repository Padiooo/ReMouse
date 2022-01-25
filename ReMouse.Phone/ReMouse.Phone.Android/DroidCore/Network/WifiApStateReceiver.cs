using Android.Content;
using Android.Net.Wifi;

namespace ReMouse.Phone.Droid.DroidCore.Network
{
    public class WifiApStateReceiver : BroadcastReceiver
    {
        public static bool WifiAP_Enabled { get; private set; }

        public override void OnReceive(Context context, Intent intent)
        {
            int state = intent.GetIntExtra(WifiManager.ExtraWifiState, 0);

            WifiAP_Enabled = 3 == state % 10;
        }
    }
}