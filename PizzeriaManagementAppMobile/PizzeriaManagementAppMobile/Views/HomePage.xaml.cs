using PizzeriaManagementAppMobile.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly IHttpClientProvider _httpClientProvider;
        public HomePage()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
            HttpClient httpClient = _httpClientProvider.GetClient();
            httpClient.BaseAddress = new Uri(WC.BaseAddress);
            var response = httpClient.GetAsync(WC.HomeIndexAddress);
            InitializeComponent();
        }
    }
}