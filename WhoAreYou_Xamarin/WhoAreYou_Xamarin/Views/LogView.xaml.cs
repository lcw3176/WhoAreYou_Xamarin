using WhoAreYou_Xamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhoAreYou_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogView : ContentPage
    {
        public LogView()
        {
            InitializeComponent();
            BindingContext = LogViewModel.GetInstance();
        }
    }
}