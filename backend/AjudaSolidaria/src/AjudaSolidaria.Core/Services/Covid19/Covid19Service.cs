using AjudaSolidaria.Domain.Data;
using CsvHelper;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AjudaSolidaria.Core.Services.Covid19
{
    public class Covid19Service : ICovid19Service
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;

        public Covid19Service(IMemoryCache cache, IHttpClientFactory httpClient)
        {
            _cache = cache;
            _httpClient = httpClient.CreateClient();
        }

        public async ValueTask<IEnumerable<Covid19Cidade>> GetCasesCitiesByStateAsync(string state)
        {
            return await _cache.GetOrCreateAsync(
                $"internal_{state}_cities_br",
                async (cache) =>
                {
                    cache.AbsoluteExpiration = DateTime.Now.AddMinutes(25);
                    var data = await GetCasesCitiesAsync();

                    return data.Where(p => p.Estado.Equals(state, StringComparison.InvariantCultureIgnoreCase));
                });
        }

        public async ValueTask<IEnumerable<Covid19Cidade>> GetCasesCitiesAsync()
        {
            return await _cache.GetOrCreateAsync(
                $"internal_cities_br",
                async (cache) =>
                {
                    cache.AbsoluteExpiration = DateTime.Now.AddMinutes(25);
                    var data = await RequestData<Covid19Cidade>("https://raw.githubusercontent.com/wcota/covid19br/master/cases-brazil-cities.csv");

                    return data;
                });
        }

        public async ValueTask<IEnumerable<Covid19Estado>> GetCasesStatesAsync()
        {
            return await _cache.GetOrCreateAsync(
                 $"internal_states_br",
                 async (cache) =>
                 {
                     cache.AbsoluteExpiration = DateTime.Now.AddMinutes(25);

                     var data = await RequestData<Covid19Estado>("https://raw.githubusercontent.com/wcota/covid19br/master/cases-brazil-states.csv");

                     return data;
                 });
        }

        public async ValueTask<IEnumerable<Covid19Total>> GetCasesTotalAsync()
        {
            return await _cache.GetOrCreateAsync(
                 $"internal_total_br",
                 async (cache) =>
                 {
                     cache.AbsoluteExpiration = DateTime.Now.AddMinutes(25);

                     var data = await RequestData<Covid19Total>("https://raw.githubusercontent.com/wcota/covid19br/master/cases-brazil-total.csv");

                     return data;
                 });
        } 

        private async ValueTask<IEnumerable<T>> RequestData<T>(string url)
        {
            try
            {
                var request = await _httpClient.GetAsync(url);
                using (var stream = await request.Content.ReadAsStreamAsync())
                {
                    var reader = new StreamReader(stream);
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<T>().ToList();
                        return records;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
