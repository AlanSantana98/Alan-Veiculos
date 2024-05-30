using System.ComponentModel.DataAnnotations;

namespace Alan_Veiculos.Models
{
    public class ClientesViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(10)]
        public string Cep { get; set; }

        [StringLength(100)]
        public string Logradouro { get; set; }

        [StringLength(100)]
        public string Bairro { get; set; }

        [StringLength(100)]
        public string Localidade { get; set; }

        [StringLength(2)]
        public string Uf { get; set; }

        [StringLength(15)]
        public string Telefone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        //public string? RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
