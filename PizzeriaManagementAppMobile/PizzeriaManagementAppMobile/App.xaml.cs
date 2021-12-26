using PizzeriaManagementAppMobile.Services.Abstract;
using PizzeriaManagementAppMobile.Services.Providers;
using PizzeriaManagementAppMobile.Services.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeDI();
            Current.Properties.Add(WC.ShoppingCart, new Dictionary<Guid, int>());
            Current.Properties.Add(WC.CurrentPizzeria, "");
            MainPage = new NavigationPage(new MainPage());
        }

        protected void InitializeDI()
        {
            DependencyService.Register<IHttpClientProvider, UnsafeHttpClientProvider>();
            DependencyService.Register<IMessageService, MessageService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
