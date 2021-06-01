using System.Collections.Generic;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models.Enum;

namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface DIBluetooth
    {
        bool IsEnable();
        Task<bool> ConnectDevice(string deviceName);
        List<string> GetAllDevicesName();
        List<string> GetAllDevicesType();
        Task<bool> SendInfo(Dictionary<BLECommand, string> data);

    }
}
