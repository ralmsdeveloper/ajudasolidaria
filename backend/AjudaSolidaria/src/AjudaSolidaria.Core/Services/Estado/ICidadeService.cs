using System.Collections.Generic;
using System.Threading.Tasks;

namespace AjudaSolidaria.Core.Services.Estado
{
    public interface ICidadeService
    {
        ValueTask<string[]> GetEstadosAsync();
        ValueTask<IEnumerable<object>> GetCidadesByEstadoAsync(string cpf); 
    }
}
