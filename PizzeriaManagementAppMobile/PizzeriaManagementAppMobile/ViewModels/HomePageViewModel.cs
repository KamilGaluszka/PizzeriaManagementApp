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
    public class HomePageViewModel
    {
        private readonly IHttpClientProvider _httpClientProvider;
        public ObservableCollection<Pizzeria> Pizzerias { get; set; } = new ObservableCollection<Pizzeria>();

        public HomePageViewModel()
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
                var url = $"{WC.BaseAddress}{WC.HomeIndexAddress}";
                var response = await httpClient.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var pizzerias = JsonConvert.DeserializeObject<List<Pizzeria>>(responseString);
                foreach (var pizzeria in pizzerias)
                {
                    Pizzerias.Add(pizzeria);
                };
            }
            catch(Exception e)
            {
                
            }
        }

    }
}
