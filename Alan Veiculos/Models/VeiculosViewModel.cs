namespace Alan_Veiculos.Models
{
    public class VeiculosViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo{ get; set; }
        public string Placa{ get; set; }
        public string Ano { get; set; }
        public decimal Valor { get; set; }


        //public string? RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
