using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void ContinueAsGuest(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}