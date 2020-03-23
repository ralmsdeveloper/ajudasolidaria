using CsvHelper.Configuration.Attributes;

namespace AjudaSolidaria.Domain.Data
{ 
    public class Covid19Estado
    {
        [Index(0)]
        public string Date { get; set; }

        [Index(1)]
        public string Pais { get; set; }

        [Index(2)]
        public string Estado { get; set; }

        [Index(3)]
        public string Cidade { get; set; }

        [Index(4)]
        public string Novos { get; set; }

        [Index(5)]
        public string Total { get; set; }
    } 
}
