using System.Collections.Generic;
using System.Threading.Tasks;

namespace AjudaSolidaria.Core.Services.Covid19
{
    public interface ICovid19Service
    {
        ValueTask<IEnumerable<Domain.Data.Covid19Total>> GetCasesTotalAsync();
        ValueTask<IEnumerable<Domain.Data.Covid19Estado>> GetCasesStatesAsync();
        ValueTask<IEnumerable<Domain.Data.Covid19Cidade>> GetCasesCitiesByStateAsync(string state);
        ValueTask<IEnumerable<Domain.Data.Covid19Cidade>> GetCasesCitiesAsync();
    }
}
