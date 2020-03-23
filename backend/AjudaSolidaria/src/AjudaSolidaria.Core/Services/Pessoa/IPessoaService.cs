using AjudaSolidaria.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using Entity = AjudaSolidaria.Domain.Entity;

namespace AjudaSolidaria.Core.Services.Pessoa
{
    public interface IPessoaService : IServiceBase<Entity.Pessoa>
    {
        Entity.Pessoa GetPessoaByCpf(string cpf);
        IEnumerable<Entity.Pessoa> GetPessoaByLocalidade(string uf, string cidade, string bairro);
    }
}
