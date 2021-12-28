using PizzeriaManagementAppMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzeriaManagementAppMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSummaryPage : ContentPage
    {
        private readonly Guid _id;
        public OrderSummaryPage(Guid id)
        {
            InitializeComponent();
            _id = id;
            var orderSummaryPageViewModel = new OrderSummaryPageViewModel(id);
            BindingContext = orderSummaryPageViewModel;
        }

        private async void Refresh(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderSummaryPage(_id));
        }

        private void BackToHomePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}