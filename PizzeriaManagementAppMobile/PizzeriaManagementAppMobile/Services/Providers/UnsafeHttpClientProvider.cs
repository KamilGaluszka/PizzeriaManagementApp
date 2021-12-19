using PizzeriaManagementAppMobile.Services.Abstract;
using System.Net.Http;

namespace PizzeriaManagementAppMobile.Services.Providers
{
    public class UnsafeHttpClientProvider : IHttpClientProvider
    {
        public HttpClient GetClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return new HttpClient(handler);
        }
    }
}
