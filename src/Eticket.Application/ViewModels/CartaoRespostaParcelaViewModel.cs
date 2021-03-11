using System;

namespace Eticket.Application.ViewModels
{
    public class CartaoRespostaParcelaViewModel
    {
        public CartaoRespostaParcelaViewModel()
        {
            CartaoRespostaParcelaViewModelGuid = Guid.NewGuid();
            DataHora = DateTime.Now;
        }
        public Guid CartaoRespostaParcelaViewModelGuid { get; set; }
        public Guid CartaoRecebimentoGuid { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
        public string NsuParcela { get; set; }
        public string NumeroParcela { get; set; }
    }
}