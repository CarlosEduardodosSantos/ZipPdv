namespace Eticket.Domain.Entity
{
    public class EspeciePagamento
    {
        public int EspeciePagamentoId { get; set; }
        public int Situacao { get; set; }
        public string Especie { get; set; }
        public bool Tef { get; set; }
        public bool Vaucher { get; set; }
        public bool Crediario { get; set; }
        public string Interno { get; set; }
        public int TipoCartao { get; set; }
        public string CodigoFiscal { get; set; }
        public string KeyAtalho { get; set; }
    }
}