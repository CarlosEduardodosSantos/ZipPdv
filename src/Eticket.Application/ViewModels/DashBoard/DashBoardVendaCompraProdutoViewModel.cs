namespace Eticket.Application.ViewModels.DashBoard
{
    public class DashBoardVendaCompraProdutoViewModel
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