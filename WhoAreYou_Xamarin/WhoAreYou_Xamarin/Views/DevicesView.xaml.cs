using WhoAreYou_Xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhoAreYou_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicesView : ContentPage
    {
        public DevicesView()
        {
            InitializeComponent();
            BindingContext = DevicesViewModel.GetInstance();
            DevicesViewModel.GetInstance().Init();
        }
    }
}