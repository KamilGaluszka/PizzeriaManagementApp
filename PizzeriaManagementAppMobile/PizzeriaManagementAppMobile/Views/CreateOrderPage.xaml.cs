using PizzeriaManagementAppMobile.Models;
using PizzeriaManagementAppMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOrderPage : ContentPage
    {
        public CreateOrderPage()
        {
            InitializeComponent();
            var vm = new CreateOrderPageViewModel
            {
                navigation = Navigation
            };
            if (Application.Current.Properties.ContainsKey(WC.CurrentUserAddress))
            {
                vm.Address = (Address)Application.Current.Properties[WC.CurrentUserAddress] ?? new Address();
            }
            BindingContext = vm;
        }
    }
}