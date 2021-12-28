using PizzeriaManagementAppMobile.Services.Abstract;
using System.Threading.Tasks;

namespace PizzeriaManagementAppMobile.Services.Services
{
    public class MessageService : IMessageService
    {
        public async Task DisplayFailAlert(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failed", message, "Ok");
        }

        public async Task DisplayOkAlert(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Success", message, "Ok");
        }
    }
}
