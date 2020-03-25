using System;
using System.Threading.Tasks;
using AjudaSolidaria.Core.Services.Pessoa;
using AjudaSolidaria.Domain.Entity;
using AjudaSolidaria.Domain.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AjudaSolidaria.Api.Controllers
{
    [ApiController]
    [Route("/v1/pessoa")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public PessoaController(
            ILogger<PessoaController> logger,
            IPessoaService pessoaService,
            IMapper mapper)
        {
            _logger = logger;
            _pessoaService = pessoaService;
            _mapper = mapper;
        } 

        [HttpPost]
        public async ValueTask<IActionResult> RegistarPessoa([FromBody]PessoaRequest request)
        {
            try
            {
                var pessoa = _mapper.Map<Pessoa>(request);
                _pessoaService.Add(pessoa);

                return Ok(await Task.FromResult(pessoa));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(request);
            }
        }

        [HttpGet]
        [Route("{key}")]
        public async ValueTask<IActionResult> GetPessoaByKey([FromRoute]Guid key)
        {
            try
            {
                var pessoa = _pessoaService.GetByKey(key);

                if(pessoa == null)
                {
                    return NotFound(new { Message = "Registro não encontrado!" });
                }

                return Ok(await Task.FromResult(pessoa));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest(key);
            }
        }

        [HttpGet]
        [Route("cpf/{cpf}")]
        public async ValueTask<IActionResult> GetPessoaByCpf([FromRoute]string cpf)
        {
            try
            {
                var pessoa = _pessoaService.GetPessoaByCpf(cpf);

                if (pessoa == null)
                {
                    return NotFound(new { Message = "Registro não encontrado!" });
                }

                return Ok(await Task.FromResult(pessoa));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest(cpf);
            }
        }

        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetPessoasByLocalidade(
            [FromQuery]string uf, 
            [FromQuery]string cidade, 
            [FromQuery]string bairro)
        {
            try
            {
                var pessoas = _pessoaService.GetPessoaByLocalidade(uf, cidade, bairro);

                return Ok(await Task.FromResult(pessoas));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest(bairro);
            }
        }
    }
}
