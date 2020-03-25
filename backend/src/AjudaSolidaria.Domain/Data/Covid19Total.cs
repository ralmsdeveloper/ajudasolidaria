using CsvHelper.Configuration.Attributes;

namespace AjudaSolidaria.Domain.Data
{
    public class Covid19Total
    {
        [Index(0)]
        public string Pais { get; set; }

        [Index(1)]
        public string Estado { get; set; }

        [Index(2)]
        public string Total { get; set; }

        [Index(3)]
        public string TotalConfirmados { get; set; }

        [Index(4)]
        public string TotalNaoConfirmados { get; set; }

        [Index(5)]
        public string TotalMortes { get; set; }
    }
}
