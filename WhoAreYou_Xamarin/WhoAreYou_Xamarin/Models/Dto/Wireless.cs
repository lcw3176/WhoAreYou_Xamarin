using System.Windows.Input;

namespace WhoAreYou_Xamarin.Models
{
    public class Wireless
    {
        public string name { get; set; }
        public string type{ get; set; }
        public ICommand itemClickCommand { get; set; }
    }
}
