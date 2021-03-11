namespace Eticket.Application.ViewModels
{
    public class CaixaFechamentoViewModel
    {
        public int CaixaFechamentoId { get; set; }
        public int CaixaId { get; set; }
        public int EspeciePagamentoId { get; set; }
        public string Especie { get; set; }
        public decimal Valor { get; set; }
        public decimal Divergencia { get; set; }
        public bool Conferido { get; set; }
        public int UsuarioConferenciaId { get; set; }
    }
}