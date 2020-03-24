using CsvHelper.Configuration.Attributes;

namespace AjudaSolidaria.Domain.Data
{
    public class Covid19Cidade
    {
        [Index(0)]
        public string Pais { get; set; }

        [Index(1)]
        public string Estado { get; set; }

        [Index(2)]
        public string Cidade { get; set; }

        [Index(3)]
        public string Total { get; set; }
    }
}
