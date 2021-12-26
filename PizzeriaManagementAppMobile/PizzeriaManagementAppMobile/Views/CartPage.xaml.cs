using PizzeriaManagementAppMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            if (!(((MenuItem)sender).BindingContext is PizzaAmount pizzaAmount))
            {
                return;
            }

            var shoppingCart = (Dictionary<Guid, int>)Application.Current.Properties[WC.ShoppingCart];
            shoppingCart[pizzaAmount.Pizza.Id]--;
            if(shoppingCart[pizzaAmount.Pizza.Id] == 0)
            {
                shoppingCart.Remove(pizzaAmount.Pizza.Id);
            }

            await Navigation.PushAsync(new CartPage());
            await DisplayAlert("Shopping cart", $"{pizzaAmount.Pizza.Name} removed from cart", "OK");
        }

        private void BackToHomePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void MakeAnOrder(object sender, EventArgs e)
        {

        }
    }
}