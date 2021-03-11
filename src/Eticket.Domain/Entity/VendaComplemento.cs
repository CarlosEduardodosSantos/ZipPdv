namespace Eticket.Domain.Entity
{
    public class VendaComplemento
    {
        public int VendaComplementoId { get; set; }
        public int ComplementoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal Valor { get; set; }
        public string Ficha { get; set; }
        public int MesaOperacaoId { get; set; }
        public int PendenteId { get; set; }
        public int VendaId { get; set; }
        public int Sequencia { get; set; }
    }
}