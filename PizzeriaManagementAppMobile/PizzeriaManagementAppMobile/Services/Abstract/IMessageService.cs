using System.Threading.Tasks;

namespace PizzeriaManagementAppMobile.Services.Abstract
{
    public interface IMessageService
    {
        Task ShowAsync(string message);
    }
}
