using Newtonsoft.Json;
using PizzeriaManagementAppMobile.Models;
using PizzeriaManagementAppMobile.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class CartPageViewModel
    {
        private readonly IHttpClientProvider _httpClientProvider;
        public ObservableCollection<PizzaAmount> Pizzas { get; set; } = new ObservableCollection<PizzaAmount>();

        public CartPageViewModel()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpClient httpClient = _httpClientProvider.GetClient();
                var url = $"{WC.BaseAddress}{WC.PizzaDetails}";
                float totalPrice = 0;
                var shoppingCart = (Dictionary<Guid, int>)Application.Current.Properties[WC.ShoppingCart];
                foreach (var item in shoppingCart)
                {
                    var response = await httpClient.GetAsync($"{url}?id={item.Key}");
                    var responseString = await response.Content.ReadAsStringAsync();
                    var pizza = JsonConvert.DeserializeObject<Pizza>(responseString);
                    Pizzas.Add(new PizzaAmount()
                    {
                        Pizza = pizza,
                        Amount = item.Value
                    });
                    totalPrice += (pizza.Price * item.Value);
                }
                Application.Current.Properties[WC.TotalCount] = totalPrice;
            }
            catch (Exception)
            {

            }
        }
    }
}
