using Newtonsoft.Json;
using PizzeriaManagementAppMobile.Models;
using PizzeriaManagementAppMobile.Services.Abstract;
using PizzeriaManagementAppMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzeriaManagementAppMobile.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IMessageService _messageService;
        private string email = "";
        private string password = "";

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; set; }
        public INavigation navigation;
        public LoginPageViewModel()
        {
            _httpClientProvider = DependencyService.Resolve<IHttpClientProvider>();
            _messageService = DependencyService.Resolve<IMessageService>();
            LoginCommand = new Command(async () => await LoginUser());
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public async Task LoginUser()
        {
            if(!IsValidEmail(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await _messageService.ShowAsync("Invalid email or password format");
            }
            else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpClient httpClient = _httpClientProvider.GetClient();
                var url = $"{WC.BaseAddress}{WC.LoginAddress}";
                string json = JsonConvert.SerializeObject(new LoginUser(Email, Password));
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.Properties[WC.CurrentUser] = Email;
                    await navigation.PushAsync(new HomePage());
                }
                else if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _messageService.ShowAsync("Email or password is invalid, try again");
                }
                else
                {
                    await _messageService.ShowAsync("Invalid login, try again");
                }
            }
        }
    }
}
