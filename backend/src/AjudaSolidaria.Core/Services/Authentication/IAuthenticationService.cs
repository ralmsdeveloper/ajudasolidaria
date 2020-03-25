using System.Threading.Tasks;

namespace AjudaSolidaria.Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        ValueTask<bool> SignInAsync(string login, string password);
        ValueTask<object> GenerateToken(); 
    }
}
