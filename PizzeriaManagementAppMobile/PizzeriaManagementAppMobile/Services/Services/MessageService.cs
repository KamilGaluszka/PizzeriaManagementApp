using PizzeriaManagementAppMobile.Services.Abstract;
using System.Threading.Tasks;

namespace PizzeriaManagementAppMobile.Services.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failed", message, "Ok");
        }
    }
}
