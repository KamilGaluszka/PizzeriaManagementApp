namespace PizzeriaManagementAppMobile.Models
{
    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
