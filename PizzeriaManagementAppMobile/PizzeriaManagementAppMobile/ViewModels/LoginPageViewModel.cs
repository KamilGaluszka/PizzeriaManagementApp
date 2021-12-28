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
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
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
                await _messageService.DisplayFailAlert("Invalid email or password format");
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
                    var url2 = $"{WC.BaseAddress}{WC.GetUserAddress}";
                    var response2 = await httpClient.GetAsync($"{url2}?email={Email}");
                    var responseString2 = await response2.Content.ReadAsStringAsync();
                    var address = JsonConvert.DeserializeObject<Address>(responseString2);
                    Application.Current.Properties[WC.CurrentUserAddress] = address;
                    await navigation.PushAsync(new HomePage());
                }
                else if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _messageService.DisplayFailAlert("Email or password is invalid, try again");
                }
                else
                {
                    await _messageService.DisplayFailAlert("Invalid login, try again");
                }
            }
        }
    }
}
