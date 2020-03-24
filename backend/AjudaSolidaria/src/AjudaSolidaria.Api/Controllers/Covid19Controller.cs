using System;
using System.Threading.Tasks;
using AjudaSolidaria.Core.Services.Covid19;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AjudaSolidaria.Api.Controllers
{
    [ApiController]
    [Route("/v1/covid19")]
    public class Covid19Controller : ControllerBase
    {
        private readonly ILogger<Covid19Controller> _logger;
        private readonly ICovid19Service _covid19Service; 

        public Covid19Controller(
            ILogger<Covid19Controller> logger,
            ICovid19Service covid19Service)
        {
            _logger = logger;
            _covid19Service = covid19Service;
        } 

        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetTotalCases()
        {
            try
            {
                var casos = await _covid19Service.GetCasesTotalAsync();
                 
                return Ok(casos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("state")]
        public async ValueTask<IActionResult> GetCasesByStates()
        {
            try
            {
                var casos = await _covid19Service.GetCasesStatesAsync();

                return Ok(casos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("state/{state}/cities")]
        public async ValueTask<IActionResult> GetCasesCitiesByState([FromRoute]string state)
        {
            try
            {
                var casos = await _covid19Service.GetCasesCitiesByStateAsync(state);

                return Ok(casos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("state/cities")]
        public async ValueTask<IActionResult> GetCasesCitiesByStat()
        {
            try
            {
                var casos = await _covid19Service.GetCasesCitiesAsync();

                return Ok(casos);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest();
            }
        }
    }
}
