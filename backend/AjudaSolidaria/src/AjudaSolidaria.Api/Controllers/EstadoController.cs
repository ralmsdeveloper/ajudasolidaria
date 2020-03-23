using System;
using System.Linq;
using System.Threading.Tasks;
using AjudaSolidaria.Core.Services.Estado;
using AjudaSolidaria.Core.Services.Pessoa;
using AjudaSolidaria.Domain.Entity;
using AjudaSolidaria.Domain.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AjudaSolidaria.Api.Controllers
{
    [ApiController]
    [Route("/v1/estado")]
    public class EstadoController : ControllerBase
    {
        private readonly ILogger<EstadoController> _logger;
        private readonly ICidadeService _cidadeService; 

        public EstadoController(
            ILogger<EstadoController> logger,
            ICidadeService cidadeService)
        {
            _logger = logger;
            _cidadeService = cidadeService;
        } 
         

        [HttpGet]
        [Route("{uf}/cidades")]
        public async ValueTask<IActionResult> GetCidades([FromRoute]string uf)
        {
            try
            {
                var cidades = await _cidadeService.GetCidadesByEstadoAsync(uf.ToUpper());
                 
                return Ok(cidades);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest(uf);
            }
        }

        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetEstados()
        {
            try
            {
                var estados = await _cidadeService.GetEstadosAsync();

                return Ok(estados);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest();
            }
        }
    }
}
