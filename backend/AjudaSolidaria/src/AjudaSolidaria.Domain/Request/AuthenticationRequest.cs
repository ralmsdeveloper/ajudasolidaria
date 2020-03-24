namespace AjudaSolidaria.Domain.Request
{
    public sealed class AuthenticationRequest
    {
        public string Login { get; set; } 
        public string Password { get; set; }
    }
}
