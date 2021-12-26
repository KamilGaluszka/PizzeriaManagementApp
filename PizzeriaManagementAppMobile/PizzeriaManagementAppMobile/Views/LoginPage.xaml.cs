using PizzeriaManagementAppMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var vm = new LoginPageViewModel();
            vm.navigation = Navigation;
            BindingContext = vm;
        }
    }
}