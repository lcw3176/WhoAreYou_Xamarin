using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface DIBluetooth
    {
        bool IsEnable();
        Task<bool> ConnectDevice(string deviceName);
        List<string> GetAllDevicesName();
        List<string> GetAllDevicesType();
        Task<bool> SetDevice(string ssid, string pw);
        bool isConnect();
        bool DisconnectDevice();

    }
}
