namespace Alan_Veiculos.Models
{
    public class UsuariosViewModel
    {

        public int Id { get; set; }
        public string Funcionario_Id { get; set; }
        public string Nome { get; set;}
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        //public string? RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
