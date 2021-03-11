namespace Zip.Pdv.Component.ProdutoGrid.Model
{
    public class ProdutoGridModel
    {
        public int ProdutoId { get; set; }
        public string ProdutoTipo { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public decimal Estoque { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal ValorVenda { get; set; }
    }
}