namespace Alan_Veiculos.Models
{
    public class FuncionariosViewModel
    {

        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set;}
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public decimal Salario { get; set; }
        public decimal Comissão { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        //public string? RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
