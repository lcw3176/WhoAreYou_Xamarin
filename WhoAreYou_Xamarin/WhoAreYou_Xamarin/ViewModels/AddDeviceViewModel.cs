using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class AddDeviceViewModel : BaseViewModel
    {
        public ObservableCollection<Bluetooths> bluetoothCollection { get; set; } = new ObservableCollection<Bluetooths>();

        public AddDeviceViewModel()
        {
            List<string> name = DependencyService.Get<IBluetoothManager>().GetAllDevicesName();
            List<string> type = DependencyService.Get<IBluetoothManager>().GetAllDevicesType();

            for(int i = 0; i < name.Count; i++)
            {
                bluetoothCollection.Add(new Bluetooths()
                {
                    name = name[i],
                    type = type[i],
                });
            }

        }

     
    }
}
