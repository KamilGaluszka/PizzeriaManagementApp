using PizzeriaManagementAppMobile.Models;
using PizzeriaManagementAppMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private readonly Pizzeria _pizzeria;
        public MenuPage(Pizzeria pizzeria)
        {
            InitializeComponent();
            _pizzeria = pizzeria ?? throw new ArgumentNullException(nameof(pizzeria));
            var menuPageViewModel = new MenuPageViewModel(pizzeria);
            BindingContext = menuPageViewModel;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            if (!(((MenuItem)sender).BindingContext is MenuVM menuVM))
            {
                return;
            }

            var actualPizzeria = (string)Application.Current.Properties[WC.CurrentPizzeria];
            if (actualPizzeria != _pizzeria.Id.ToString())
            {
                Application.Current.Properties[WC.ShoppingCart] = new Dictionary<Guid, int>();
            }
            Application.Current.Properties[WC.CurrentPizzeria] = _pizzeria.Id.ToString();
            var shoppingCart = (Dictionary<Guid, int>)Application.Current.Properties[WC.ShoppingCart];
            if (!shoppingCart.ContainsKey(menuVM.Pizza.Id))
            {
                shoppingCart.Add(menuVM.Pizza.Id, 1);
            }
            else
            {
                shoppingCart[menuVM.Pizza.Id]++;
            }
            await DisplayAlert("Shopping cart", $"{menuVM.Pizza.Name} added to cart", "OK");
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(((ListView)sender).SelectedItem is MenuVM menuVM))
            {
                return;
            }

            var actualPizzeria = (string)Application.Current.Properties[WC.CurrentPizzeria];
            if(actualPizzeria != _pizzeria.Id.ToString())
            {
                Application.Current.Properties[WC.ShoppingCart] = new Dictionary<Guid, int>();
            }
            Application.Current.Properties[WC.CurrentPizzeria] = _pizzeria.Id.ToString();
            var shoppingCart = (Dictionary<Guid, int>)Application.Current.Properties[WC.ShoppingCart];
            if (!shoppingCart.ContainsKey(menuVM.Pizza.Id))
            {
                shoppingCart.Add(menuVM.Pizza.Id, 1);
            }
            else
            {
                shoppingCart[menuVM.Pizza.Id]++;
            }
            await DisplayAlert("Shopping cart", $"{menuVM.Pizza.Name} added to cart", "OK");
        }
    }
}