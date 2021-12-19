using PizzeriaManagementAppMobile.Services.Abstract;
using PizzeriaManagementAppMobile.Services.Providers;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeDI();
            MainPage = new NavigationPage(new MainPage());
        }

        protected void InitializeDI()
        {
            DependencyService.Register<IHttpClientProvider, UnsafeHttpClientProvider>();
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
