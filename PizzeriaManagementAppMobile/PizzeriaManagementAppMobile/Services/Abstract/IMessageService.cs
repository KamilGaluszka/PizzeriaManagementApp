using System.Threading.Tasks;

namespace PizzeriaManagementAppMobile.Services.Abstract
{
    public interface IMessageService
    {
        Task DisplayFailAlert(string message);
        Task DisplayOkAlert(string message);
    }
}
