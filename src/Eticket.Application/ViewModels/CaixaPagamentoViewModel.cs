using System;

namespace Eticket.Application.ViewModels
{
    public class CaixaPagamentoViewModel
    {
        public CaixaPagamentoViewModel()
        {
            CartaoResposta = new CartaoRespostaViewModel();
        }
        public int CaixaPagamentoId { get; set; }
        public int CaixaId { get; set; }
        public Guid CaixaItemId { get; set; }
        public Guid CartaoRespostaGuid { get; set; }
        public int EspeciePagamentoId { get; set; }
        public string Especie { get; set; }
        public decimal Valor { get; set; }
        public string Interno { get; set; }
        public string CodigoFiscal { get; set; }
        public string Vaucher { get; set; }
        public CartaoRespostaViewModel CartaoResposta { get; set; }
        public ClienteViewModel Cliente { get; set; }
    }
}