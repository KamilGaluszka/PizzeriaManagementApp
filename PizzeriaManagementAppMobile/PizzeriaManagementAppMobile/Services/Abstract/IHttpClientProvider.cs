using System.Net.Http;

namespace PizzeriaManagementAppMobile.Services.Abstract
{
    public interface IHttpClientProvider
    {
        HttpClient GetClient();
    }
}
