namespace TnR_SS.API.Authentication.Model
{
    public class LoginModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class ResponseLoginModel
    {
        public string Token { get; set; }
        public string UserID { get; set; }
    }
}
