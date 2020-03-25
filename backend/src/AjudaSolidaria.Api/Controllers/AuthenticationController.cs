using System;
using System.Threading.Tasks;
using AjudaSolidaria.Core.Services.Authentication;
using AjudaSolidaria.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AjudaSolidaria.Api.Controllers
{
    [ApiController]
    [Route("/v1/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> Authentication([FromBody]AuthenticationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Login))
                {
                    throw new ArgumentNullException();
                }

                if (string.IsNullOrEmpty(request.Password))
                {
                    throw new ArgumentNullException();
                }

                if(!await _authenticationService.SignInAsync(request.Login, request.Password))
                {
                    return new ForbidResult(); 
                }

                var token = await _authenticationService.GenerateToken(); 

                return Ok(new { token });

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(request);
            }
        }
    }
}
