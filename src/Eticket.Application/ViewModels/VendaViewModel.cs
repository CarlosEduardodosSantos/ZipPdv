﻿using System;
using System.Collections.Generic;
using System.Linq;
using Eticket.Domain.Entity;

namespace Eticket.Application.ViewModels
{
    public class VendaViewModel
    {
        public VendaViewModel()
        {
            DataHora = DateTime.Now;
            VendaItens = new List<VendaItemViewModel>();
            VendaFinalizadora = new List<CaixaPagamentoViewModel>();
            Delivery = new DeliveryViewModel();
        }

        public int  VendaId { get; set; }
        public int Loja { get; set; }
        public int Pdv { get; set; }
        public int CaixaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string TipoPagamento { get; set; }
        public int UsuarioId { get; set; }
        public decimal ValorCompra { get; set; }
        public string Cnpj { get; set; }
        public string CupomFiscal { get; set; }
        public string Fidelidade { get; set; }
        public string Observacao { get; set; }
        public string Senha { get; set; }
        public string MenssagemSat { get; set; }
        public List<VendaItemViewModel> VendaItens { get; set; }
        public List<CaixaPagamentoViewModel> VendaFinalizadora { get; set; }
        public ClienteDeliveryViewModel ClienteDelivery { get; set; }
        public DeliveryViewModel Delivery { get; set; }
        public bool IsDelivery { get; set; }
        public decimal ValorTotal {
            get { return VendaItens.Sum(t => t.SubTotal); }
        }
        public decimal ValorFinal
        {
            get { return VendaItens.Sum(t => t.ValorTotal); }
        }
        public decimal DescontoPercentual { get; set; }

        public void AdicionarFichaItemToVendaItem(VendaFichaViewModel vendaFichaView)
        {
            var item = VendaItens.FirstOrDefault(t => t.ProdutoId == vendaFichaView.ProdutoId &&
                                             t.Produto == vendaFichaView.NomeProduto);

            if (item != null)
            {
                item.Quantidade += vendaFichaView.Quantidade;
                return;
            }
 
            VendaItens.Add(new VendaItemViewModel()
            {
                ProdutoId = vendaFichaView.ProdutoId,
                Produto = vendaFichaView.NomeProduto,
                SeqProduto = vendaFichaView.Sequencia,
                Observacao = vendaFichaView.Observacao,
                ValorUnitatio = vendaFichaView.ValorUnitatio,
                Quantidade = vendaFichaView.Quantidade
            });

        }
    }
}