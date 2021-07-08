namespace Eticket.Domain.Entity.DashBoard
{
    public class DashBoardProduto
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeVendida { get; set; }
        public decimal QuantidadeMim { get; set; }
        public decimal QuantidadeMax { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCusto { get; set; }
    }
}