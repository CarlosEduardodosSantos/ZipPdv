using System;
using System.Collections.Generic;

namespace Eticket.Domain.Entity
{
    public class Venda
    {

        public int VendaId { get; set; }
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
        public int ModeloFiscal { get; set; }
        public string Observacao { get; set; }
        public string Senha { get; set; }
        public string MenssagemSat { get; set; }
        public List<VendaItem> VendaItens { get; set; }
        public Delivery Delivery { get; set; }
        public bool IsDelivery { get; set; }
        public string ClientePendencia { get; set; }
        public int PendenciaId { get; set; }
        public string FichaId { get; set; }
        public int[] Fichas { get; set; }

    }
}