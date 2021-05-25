using System;
using System.Collections.Generic;
using System.Linq;

namespace Eticket.Application.ViewModels
{
    public class VendaItemViewModel
    {
        public VendaItemViewModel()
        {
            VendaComplementos = new List<VendaComplementoViewModel>();
        }
        public Guid Guid { get; }
        public int VendaItemId { get; set; }
        public int VendaId { get; set; }
        public int SeqProduto { get; set; }
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public ProdutoViewModel ProdutoViewModel { get; set; }
        public decimal ValorUnitatio { get; set; }
        public decimal ValorDe { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Desconto { get; set; }
        public decimal DescontoPercentual { get; set; }
        public decimal Adicional { get; set; }
        public decimal SubTotal => (Quantidade * ValorUnitatio);
        public decimal ValorTotal => decimal.Round(((ValorUnitatio * Quantidade) + Adicional )- Desconto, 2);
        public string Observacao { get; set; }
        public ICollection<VendaComplementoViewModel> VendaComplementos { get; set; }
        public string ComplementoDescricao => ObterComplementoDescricao();
        public string DescricaoProduto => ObterDescricaoProduto();

        private string ObterComplementoDescricao()
        {
            string obs = string.Empty;
            if (VendaComplementos == null) return obs;
            string separator = " + ";
            return VendaComplementos.Aggregate(obs, (current, subItem) => current + (separator + subItem.Descricao)) + " ";
        }
        private string ObterDescricaoProduto()
        {
            var obs = Produto;
            if (VendaComplementos.Count == 0) return obs;

            var separator = " + ";
            return VendaComplementos.Aggregate(obs, (current, subItem) => current + (separator + subItem.Descricao)) + " ";
        }
    }
}