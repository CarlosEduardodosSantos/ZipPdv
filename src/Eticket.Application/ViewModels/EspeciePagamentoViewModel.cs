namespace Eticket.Application.ViewModels
{
    public class EspeciePagamentoViewModel
    {
        public int EspeciePagamentoId { get; set; }
        public int Situacao { get; set; }
        public string Especie { get; set; }
        public bool Tef { get; set; }
        public bool Vaucher { get; set; }
        public bool Crediario { get; set; }
        public string Interno { get; set; }
        public EspecieCartaoTipoEnumView TipoCartao { get; set; }
        public string CodigoFiscal { get; set; }
    }
}