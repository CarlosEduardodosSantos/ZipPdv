namespace Eticket.Domain.Entity
{
    public class CaixaFechamento
    {
        public int CaixaFechamentoId { get; set; }
        public int CaixaId { get; set; }
        public int EspecieId { get; set; }
        public string Especie { get; set; }
        public decimal Valor { get; set; }
        public decimal Divergencia { get; set; }
        public bool Conferido { get; set; }
        public int UsuarioConferenciaId { get; set; }
    }
}