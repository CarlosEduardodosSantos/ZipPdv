using System.Collections.Generic;

namespace Eticket.Domain.Entity
{
    public class VendaItem
    {
        public VendaItem()
        {
            VendaComplementos = new List<VendaComplemento>();
        }
        public int VendaItemId { get; set; }
        public int VendaId { get; set; }
        public int SeqProduto { get; set; }
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public decimal ValorUnitatio { get; set; }
        public decimal ValorCusto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PesoQuantidadeFixo { get; set; }
        public decimal Desconto { get; set; }
        public decimal Adicional { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public string DescricaoProduto { get; set; }
        public ICollection<VendaComplemento> VendaComplementos { get; set; }
        public ICollection<VendaProdutoOpcao> VendaProdutoOpcoes { get; set; }
    }
}