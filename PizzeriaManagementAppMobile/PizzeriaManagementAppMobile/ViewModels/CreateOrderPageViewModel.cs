using Newtonsoft.Json;
using PizzeriaManagementAppMobile.Models;
using PizzeriaManagementAppMobile.Services.Abstract;
using PizzeriaManagementAppMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class CreateOrderPageViewModel : INotifyPropertyChanged
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IMessageService _messageService;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateOrderCommand { get; set; }
        public INavigation navigation;
        public Address Address { get; set; }
        public string PaymentMethod { get; set; }
        public CreateOrderPageViewModel()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
            _messageService = DependencyService.Resolve<IMessageService>();
            CreateOrderCommand = new Command(async () => await CreateOrder());
        }

        public string TotalPrice
        {
            get
            {
                float totalPrice = (float)Application.Current.Properties[WC.TotalCount];
                return $"Total price: {totalPrice}";
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task CreateOrder()
        {
            if (Address != null && 
                !string.IsNullOrWhiteSpace(Address?.Country) && 
                !string.IsNullOrWhiteSpace(Address?.PostalCode) && 
                !string.IsNullOrWhiteSpace(Address?.Street) && 
                !string.IsNullOrWhiteSpace(Address?.HouseNumber) && 
                !string.IsNullOrWhiteSpace(Address?.Town) &&
                !string.IsNullOrWhiteSpace(PaymentMethod)
                )
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpClient httpClient = _httpClientProvider.GetClient();
                var url = $"{WC.BaseAddress}{WC.OrderAPI}";
                string email = (string)Application.Current.Properties[WC.CurrentUser];
                float totalPrice = (float)Application.Current.Properties[WC.TotalCount];
                Guid pizzeriaId = new Guid((string)Application.Current.Properties[WC.CurrentPizzeria]);
                List<PizzaAmountVM> pizzaAmounts = new List<PizzaAmountVM>();
                var shoppingCart = (Dictionary<Guid, int>)Application.Current.Properties[WC.ShoppingCart];
                foreach (var item in shoppingCart)
                {
                    pizzaAmounts.Add(new PizzaAmountVM()
                    {
                        PizzaId = item.Key,
                        Amount = item.Value
                    });
                }
                string json = JsonConvert.SerializeObject(new OrderAPI() 
                { 
                    Email =  email, 
                    TotalPrice = totalPrice, 
                    Address = Address, PizzaAmounts = pizzaAmounts,
                    PizzeriaId = pizzeriaId,
                    Payment = PaymentMethod
                });
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Guid id = JsonConvert.DeserializeObject<Guid>(responseString);
                    Application.Current.Properties[WC.ShoppingCart] = new Dictionary<Guid, int>();
                    await _messageService.DisplayOkAlert("Your order was created");
                    await navigation.PushAsync(new OrderSummaryPage(id));
                }
                else
                {
                    await _messageService.DisplayFailAlert("Something went wrong, try again");
                }
            }
            else
            {
                await _messageService.DisplayFailAlert("Please fill address and payment method corectly");
            }
        }
    }
}
