namespace Eticket.Domain.Entity
{
    public class ProdutoComplemento
    {
        public int ComplementoId { get; set; }
        public int GrupoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}