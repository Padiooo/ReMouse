using Android.Bluetooth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ReMouse.Phone.Core.Network;

[assembly: Dependency(typeof(ReMouse.Phone.Droid.DroidCore.Network.BluetoothHandler))]

namespace ReMouse.Phone.Droid.DroidCore.Network
{
    public class BluetoothHandler : IBluetoothHandler
    {
        public bool IsSupported { get; }

        private bool isAvailable;
        public bool IsAvailable
        {
            get => isAvailable;
            private set
            {
                if (isAvailable == value)
                    return;
                isAvailable = value;
                AvailabilityChanged?.Invoke(this, value);
            }
        }

        public ObservableCollection<string> Devices { get; } = new ObservableCollection<string>();
        private readonly List<BluetoothDevice> devices = new List<BluetoothDevice>();

        public event EventHandler<bool> AvailabilityChanged;

        private readonly BluetoothAdapter adapter;

        public BluetoothHandler()
        {
            IsSupported = BluetoothAdapter.DefaultAdapter != null;

            if (!IsSupported)
                return;

            adapter = BluetoothAdapter.DefaultAdapter;

            IsAvailable = adapter.IsEnabled;

            if (IsAvailable)
                GetDevices();
        }

        public void Refresh()
        {
            if (!IsSupported)
                return;

            IsAvailable = adapter.IsEnabled;
            GetDevices();
        }

        private void GetDevices()
        {
            devices.Clear();
            Devices.Clear();
            foreach (var item in adapter.BondedDevices)
            {
                BluetoothDevice device = new BluetoothDevice(item);
                devices.Add(device);
                Devices.Add(device.Name);
            }
        }

        public Task<ISocket> ConnectAsync(string deviceName)
        {
            return Task.Run<ISocket>(async () =>
            {
                BluetoothDevice device = devices.First(d => d.Name == deviceName);

                if (device == null)
                    return null;

                await device.ConnectAsync();

                return device.Connected ? device : null;
            });
        }
    }
}