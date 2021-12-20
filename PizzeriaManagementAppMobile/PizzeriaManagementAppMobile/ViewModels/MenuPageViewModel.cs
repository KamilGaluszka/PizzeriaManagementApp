using Newtonsoft.Json;
using PizzeriaManagementAppMobile.Models;
using PizzeriaManagementAppMobile.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class MenuPageViewModel : BindableObject
    {
        private readonly IHttpClientProvider _httpClientProvider;
        public ObservableCollection<MenuVM> Pizzas { get; set; } = new ObservableCollection<MenuVM>();
        public Pizzeria Pizzeria { get; set; }

        public MenuPageViewModel(Pizzeria pizzeria)
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
            Pizzeria = pizzeria ?? throw new ArgumentNullException(nameof(pizzeria));
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpClient httpClient = _httpClientProvider.GetClient();
                var url = $"{WC.BaseAddress}{WC.HomeMenuAddress}";
                var response = await httpClient.GetAsync($"{url}?id={Pizzeria.Id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var pizzas = JsonConvert.DeserializeObject<List<MenuVM>>(responseString);
                foreach (var pizza in pizzas)
                {
                    Pizzas.Add(pizza);
                };
            }
            catch (Exception e)
            {

            }
        }
    }
}
