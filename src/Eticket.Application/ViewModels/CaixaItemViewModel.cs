using System;
using System.Collections.Generic;

namespace Eticket.Application.ViewModels
{
    public class CaixaItemViewModel
    {
        public CaixaItemViewModel()
        {
            CaixaItemId = Guid.NewGuid();
            CaixaPagamentos = new List<CaixaPagamentoViewModel>();
            CartaoRespostas = new List<CartaoRespostaViewModel>();
        }
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
        public ICollection<CaixaPagamentoViewModel> CaixaPagamentos { get; set; }
        public virtual ICollection<CartaoRespostaViewModel> CartaoRespostas { get; set; }
    }
}