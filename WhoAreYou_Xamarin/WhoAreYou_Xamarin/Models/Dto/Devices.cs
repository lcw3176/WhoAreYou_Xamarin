using System.Windows.Input;

namespace WhoAreYou_Xamarin.Models
{
    class Devices
    {
        public int index { get; set; }
        public string name { get; set; }
        public ICommand searchLogCommand { get; set; }
        public ICommand deleteCommand { get; set; }
    }
}
