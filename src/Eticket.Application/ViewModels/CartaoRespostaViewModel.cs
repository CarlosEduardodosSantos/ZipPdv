using System;
using System.Collections.Generic;

namespace Eticket.Application.ViewModels
{
    public class CartaoRespostaViewModel
    {
        public CartaoRespostaViewModel()
        {
            CartaoRespostaGuid = Guid.NewGuid();
            DataHora = DateTime.Now;
            LoteAutorizacao = string.Empty;
            CnpjRede = string.Empty;
            QuantidadeParcela = 0;
            NumeroCartao = string.Empty;
            CodigoSat = string.Empty;
            CodigoAutorizacao = string.Empty;
            Vinculado = string.Empty;
            Bandeira = string.Empty;
            NomeRede = string.Empty;
            CodigoNsu = string.Empty;
            DataComprovante = DateTime.Now;
            DataTransacao = DateTime.Now;
            Operadora = string.Empty;
            TipoOperacao = 0;
            Comprovante = string.Empty;
            Menssagem = String.Empty;
            Comprovantes = new List<ComprovanteTef>();
        }
        public Guid CartaoRespostaGuid { get; set; }
        public CartaoTipoOperacaoEnumView TipoOperacao { get; set; }
        public bool Autorizado { get; set; }
        public string CodigoAutorizacao { get; set; }
        public DateTime DataHora { get; set; }
        public int Requisicao { get; set; }
        public int RequisicaoCancelamento { get; set; }
        public string Vinculado { get; set; }
        public string Bandeira { get; set; }
        public string NomeRede { get; set; }
        public string CnpjRede { get; set; }
        public decimal Valor { get; set; }
        public string CodigoNsu { get; set; }
        public string NumeroCartao { get; set; }
        public decimal ValorRestante { get; set; }
        public string LoteAutorizacao { get; set; }
        public bool Parcelada { get; set; }
        public bool ParceladaLoja { get; set; }
        public int QuantidadeParcela { get; set; }
        public string CodigoSat { get; set; }
        public DateTime DataTransacao { get; set; }
        public DateTime DataComprovante { get; set; }
        public string Operadora { get; set; }
        public CartaoTipoTransacaoEnumView TipoTransacao { get; set; }
        public string Comprovante { get; set; }
        public List<ComprovanteTef> Comprovantes { get; set; }
        public string Menssagem { get; set; }
        public virtual ICollection<CartaoRespostaParcelaViewModel> CartaoRespostaoParcelas { get; set; }
    }

    public class ComprovanteTef
    {
        public string Comprovante { get; set; }
    }
}