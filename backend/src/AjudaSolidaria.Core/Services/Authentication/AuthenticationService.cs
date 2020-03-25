using AjudaSolidaria.Core.Services.Pessoa;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AjudaSolidaria.Core.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPessoaService _pessoaService;
        private readonly IConfiguration _configuration;
        private Domain.Entity.Pessoa _pessoa;

        public AuthenticationService(IPessoaService pessoaService, IConfiguration configuration)
        {
            _pessoaService = pessoaService;
            _configuration = configuration;
        }

        public async ValueTask<object> GenerateToken()
        {
            if(_pessoa == null)
            {
                throw new AuthenticationException();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);
            var expires = DateTime.UtcNow.AddHours(3);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _pessoa.Nome),
                    new Claim("Estado", _pessoa.Estado ?? string.Empty),
                    new Claim("Cidade", _pessoa.Cidade ?? string.Empty),
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return await Task.FromResult(new
            {
                token,
                expires
            });
        }

        public async ValueTask<bool> SignInAsync(string login, string password)
        {
            _pessoa = _pessoaService.GetPessoaByCpf(login);

            if(_pessoa == null)
            {
                return false;
            }
             
            var dataNascimento = _pessoa.DataNascimento.ToString("ddMMyyyy");

            if(password != dataNascimento)
            {
                return false;
            }

            return await Task.FromResult(true);
        }
    }
}
