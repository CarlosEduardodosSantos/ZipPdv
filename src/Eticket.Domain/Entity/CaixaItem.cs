using System;
using System.Collections.Generic;

namespace Eticket.Domain.Entity
{
    public class CaixaItem
    {
        public Guid CaixaItemId { get; set; }
        public int CaixaId { get; set; }
        public int VendaId { get; set; }
        public int UsuarioId { get; set; }
        public int FaturamentoId { get; set; }
        public decimal Valor { get; set; }
        public decimal Troco { get; set; }
        public DateTime DataHora { get; set; }
        public string Historico { get; set; }
        public string TipoLancamento { get; set; }
        public ICollection<CaixaPagamento> CaixaPagamentos { get; set; }
    }
}