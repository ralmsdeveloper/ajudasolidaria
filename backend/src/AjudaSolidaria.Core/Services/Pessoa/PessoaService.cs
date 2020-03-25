using AjudaSolidaria.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using Entity = AjudaSolidaria.Domain.Entity;

namespace AjudaSolidaria.Core.Services.Pessoa
{
    public class PessoaService : IPessoaService
    {
        private readonly AjudaSolidariaContext _db;

        public PessoaService(AjudaSolidariaContext db)
        {
            _db = db;
        }

        public void Add(Entity.Pessoa entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public Entity.Pessoa GetByKey(Guid key)
        {
            var pessoa = _db
               .Set<Entity.Pessoa>()
               .FirstOrDefault(p => p.Key == key);

            return pessoa;
        }

        public IEnumerable<Entity.Pessoa> GetPessoaByLocalidade(string uf, string cidade, string bairro)
        {
            var query = _db.Set<Entity.Pessoa>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(uf))
            {
                query = query.Where(p => p.Estado == uf);
            }

            if (!string.IsNullOrWhiteSpace(cidade))
            {
                query = query.Where(p => p.Cidade.Contains(cidade));
            }

            if (!string.IsNullOrWhiteSpace(bairro))
            {
                query = query.Where(p => p.Bairro.Contains(bairro));
            }

            var pessoas = query
                .OrderBy(p => p.Estado)
                .ThenBy(p => p.Cidade)
                .ThenBy(p => p.Bairro);

            return pessoas;
        }

        public Entity.Pessoa GetPessoaByCpf(string cpf)
        {
            var pessoa = _db
               .Set<Entity.Pessoa>()
               .FirstOrDefault(p => p.CPF == cpf);

            return pessoa;
        }

        public IEnumerable<Entity.Pessoa> GetPessoaByEstado(string uf)
        {
            var pessoas = _db
               .Set<Entity.Pessoa>()
               .Where(p => p.Estado == uf);

            return pessoas;
        }
    }
}
