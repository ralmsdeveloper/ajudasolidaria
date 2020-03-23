using AjudaSolidaria.Domain.Entity;
using AjudaSolidaria.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjudaSolidaria.Core.Services.Estado
{
    public class CidadeService : ICidadeService
    {
        private readonly AjudaSolidariaContext _db;
        private readonly IMemoryCache _cache;

        public CidadeService(
            AjudaSolidariaContext db,
            IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async ValueTask<IEnumerable<object>> GetCidadesByEstadoAsync(string uf)
        {
            return await _cache.GetOrCreateAsync(
                $"internal_cidades_{uf}",
                async (cache) =>
                {
                    cache.AbsoluteExpiration = DateTime.Now.AddHours(24);

                    return await _db
                        .Set<Cidade>()
                        .Where(p => p.UF == uf)
                        .OrderBy(p => p.Nome)
                        .Select(p => new
                        {
                            Cidade = p.Nome,
                            SiglaUF = p.UF
                        })
                        .ToListAsync();
                });
        }

        public async ValueTask<string[]> GetEstadosAsync()
        {
            return await _cache.GetOrCreateAsync(
                "internal_estados",
                async (cache) =>
            {
                cache.AbsoluteExpiration = DateTime.Now.AddHours(24);

                return await _db
                    .Set<Cidade>()
                    .GroupBy(p => p.UF)
                    .Select(p => p.Key)
                    .OrderBy(p => p)
                    .ToArrayAsync();
            });
        }
    }
}
