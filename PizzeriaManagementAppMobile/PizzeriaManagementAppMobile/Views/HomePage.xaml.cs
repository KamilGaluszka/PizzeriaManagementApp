using PizzeriaManagementAppMobile.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(((ListView)sender).SelectedItem is Pizzeria pizzeria))
            {
                return;
            }

            await Navigation.PushAsync(new MenuPage(pizzeria));
        }
    }
}