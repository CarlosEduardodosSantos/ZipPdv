namespace Eticket.Domain.Entity
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string ProdutoTipo { get; set; }
        public int GrupoId { get; set; }
        public string Unidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCusto { get; set; }
        public bool ParaBalanca { get; set; }
        public bool QuantidadeFixo { get; set; }
        public bool Visivel { get; set; }
    }
}