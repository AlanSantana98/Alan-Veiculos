namespace Alan_Veiculos.Models
{
    public class VendasViewModel
    {
        public int Id { get; set; }
        public int Funcionario_Id { get; set; }
        public int Veiculo_Id { get; set; }
        public int Cliente_Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data_Hora { get; set; }
        public decimal Comissao { get; set; }
        public int Garantia { get; set; }

        //public string? RequestId { get; set; }
        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
