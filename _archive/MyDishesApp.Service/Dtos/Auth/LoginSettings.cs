namespace MyDishesApp.Service.Dtos.Auth
{
    public class LoginSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
    }
}
