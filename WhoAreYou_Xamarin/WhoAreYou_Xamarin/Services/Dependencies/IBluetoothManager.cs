using System.Collections.Generic;

namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface IBluetoothManager
    {
        List<string> GetAllDevicesName();
        List<string> GetAllDevicesType();
    }
}
