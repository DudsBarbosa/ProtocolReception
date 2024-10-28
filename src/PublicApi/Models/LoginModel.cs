namespace ProtocolReception.PublicApi.Models
{
    public class LoginModel
    {
        public string? Username { get; internal set; }
        public string? Password { get; internal set; }
        public LoginModel(string? username, string? password)
        {
            Username = username;
            Password = password;
        }
    }
}