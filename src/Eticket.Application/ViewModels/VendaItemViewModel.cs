﻿using System.Collections.Generic;
using System.Linq;

namespace Eticket.Application.ViewModels
{
    public class VendaItemViewModel
    {
        public VendaItemViewModel()
        {
            VendaComplementos = new List<VendaComplementoViewModel>();
            VendaProdutoOpcoes = new List<VendaProdutoOpcaoViewModel>();
            VendaProdutoMeioMeio = new List<VendaProdutoOpcaoViewModel>();
        }
        public int VendaItemId { get; set; }
        public int VendaId { get; set; }
        public int SeqProduto { get; set; }
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public decimal ValorUnitatio { get; set; }
        public decimal ValorDe { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PesoQuantidadeFixo { get; set; }
        public decimal Desconto { get; set; }
        public decimal DescontoPercentual { get; set; }
        public decimal Adicional { get; set; }
        public decimal SubTotal => (Quantidade * ValorUnitatio);
        public decimal ValorTotal => decimal.Round(((ValorUnitatio * Quantidade) + Adicional )- Desconto, 2);
        public string Observacao { get; set; }
        public ProdutoViewModel ProdutoViewModel { get; set; }
        public ICollection<VendaComplementoViewModel> VendaComplementos { get; set; }
        public ICollection<VendaProdutoOpcaoViewModel> VendaProdutoOpcoes { get; set; }
        public ICollection<VendaProdutoOpcaoViewModel> VendaProdutoMeioMeio { get; set; }
        public string ComplementoDescricao => ObterComplementoDescricao();
        public string DescricaoProduto => ObterDescricaoProduto();
        public string DescricaoProdutoMeioMeio => ObterDescricaoProdutoMeioMeio();

        private string ObterComplementoDescricao()
        {
            string obs = string.Empty;
            if (VendaComplementos == null) return obs;
            string separator = " + ";
            return VendaComplementos.Aggregate(obs, (current, subItem) => current + (separator + subItem.Descricao)) + "\n ";
        }
        private string ObterDescricaoProduto()
        {
            var obs = Produto;
            //if (VendaComplementos.Count == 0 && VendaProdutoOpcoes.Count == 0) return obs;

            var separator = "\n + ";
            if (VendaComplementos.Count() > 0)
                obs = VendaComplementos.Aggregate(obs, (current, subItem) => current + (separator + subItem.Descricao)) + "\n";

            if (VendaProdutoOpcoes.Count() > 0)
                obs = VendaProdutoOpcoes.Aggregate(obs, (current, subItem) => current + ($"{separator} {subItem.Quantidade} {subItem.Descricao}")) + System.Environment.NewLine;

            if (!string.IsNullOrEmpty(Observacao))
                obs += $" {Observacao}";

            return obs;
        }
        private string ObterDescricaoProdutoMeioMeio()
        {
            var obs = "";
            //if (VendaComplementos.Count == 0 && VendaProdutoOpcoes.Count == 0) return obs;

            var separator = "\n";
            if (VendaComplementos.Count() > 0)
                obs = VendaComplementos.Aggregate(obs, (current, subItem) => current + (separator + subItem.Descricao)) + "\n";

            if (VendaProdutoMeioMeio.Count() > 0)
                obs = VendaProdutoMeioMeio.Aggregate(obs, (current, subItem) => current + ($"{subItem.Descricao} {separator}"));

            if (!string.IsNullOrEmpty(Observacao))
                obs += $" {Observacao}";

            return obs;
        }
    }
}