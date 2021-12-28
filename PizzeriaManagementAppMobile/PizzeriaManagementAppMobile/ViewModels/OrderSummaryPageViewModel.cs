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
    public class OrderSummaryPageViewModel
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly Guid _id;
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public OrderSummaryPageViewModel(Guid id)
        {
            _id = id != Guid.Empty ? id : throw new ArgumentNullException(nameof(id));
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpClient httpClient = _httpClientProvider.GetClient();
                var url = $"{WC.BaseAddress}{WC.OrderDetailsAPI}";
                var response = await httpClient.GetAsync($"{url}?id={_id}");
                var responseString = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(responseString);
                Orders.Add(order);
            }
            catch (Exception)
            {

            }
        }
    }
}
