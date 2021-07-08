namespace Eticket.Domain.Entity.DashBoard
{
    public class DashBoardVendaCompraProduto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCusto { get; set; }
    }
}