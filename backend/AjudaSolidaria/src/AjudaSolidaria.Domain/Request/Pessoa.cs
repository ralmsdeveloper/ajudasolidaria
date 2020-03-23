using System;

namespace AjudaSolidaria.Domain.Request
{
    public sealed class PessoaRequest
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Coordenadas { get; set; }
        public bool Status { get; set; }
    }
}
