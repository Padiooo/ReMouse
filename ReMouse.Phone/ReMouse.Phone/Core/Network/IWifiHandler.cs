
using System.Threading.Tasks;

namespace ReMouse.Phone.Core.Network
{
    public interface IWifiHandler : IHandler
    {
        /// <summary>
        /// Try to connect to the given ip.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>Null if connection failed.</returns>
        Task<ISocket> ConnectAsync(string ip);
    }
}
