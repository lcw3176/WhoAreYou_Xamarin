
using WhoAreYou_Xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhoAreYou_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDeviceView : ContentPage
    {
        public AddDeviceView()
        {
            InitializeComponent();
            BindingContext = new AddDeviceViewModel();
        }
    }
}