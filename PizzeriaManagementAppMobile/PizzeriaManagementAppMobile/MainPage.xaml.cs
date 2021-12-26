using PizzeriaManagementAppMobile.Views;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Navigation.PushAsync(new StartPage());
        }
    }
}
