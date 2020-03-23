using System;
using System.Threading.Tasks;
using AjudaSolidaria.Domain.Entity;
using AjudaSolidaria.Domain.Request;
using AjudaSolidaria.Respository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AjudaSolidaria.Api.Controllers
{
    [ApiController]
    [Route("/v1/pessoa")]
    public class CadastroController : ControllerBase
    {
        private readonly ILogger<CadastroController> _logger;
        private readonly AjudaSolidariaContext _db;
        private readonly IMapper _mapper;

        public CadastroController(
            ILogger<CadastroController> logger,
            AjudaSolidariaContext db,
            IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        } 

        [HttpPost]
        public async ValueTask<IActionResult> RegistarPessoa([FromBody]PessoaRequest request)
        {
            try
            {
                var pessoa = _mapper.Map<Pessoa>(request);

                _db.Add(pessoa);
                
                await _db.SaveChangesAsync();

                return Ok(pessoa);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(request);
            }
        }

        [HttpGet]
        [Route("{key}")]
        public async ValueTask<IActionResult> GetPessoa([FromRoute]Guid key)
        {
            try
            {
                var pessoa = await _db.Set<Pessoa>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p=>p.Key == key);

                return Ok(pessoa);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return BadRequest(key);
            }
        }
    }
}
