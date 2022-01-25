using System;
using System.Threading.Tasks;

namespace ReMouse.Phone.Core.Network
{
    public interface ISocket
    {
        bool Connected { get; }

        event EventHandler<bool> ConnectionChanged;
        event EventHandler<DataReceivedArgs> DataReceived;

        Task DisconnectAsync();
        Task SendAsync(byte[] buffer, int offset, int length);
    }
}
