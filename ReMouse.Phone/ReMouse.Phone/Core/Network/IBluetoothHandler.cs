using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ReMouse.Phone.Core.Network
{
    public interface IBluetoothHandler : IHandler
    {
        ObservableCollection<string> Devices { get; }

        /// <summary>
        /// Try to connect to given device.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>Null if connection failed.</returns>
        Task<ISocket> ConnectAsync(string deviceName);
    }
}
