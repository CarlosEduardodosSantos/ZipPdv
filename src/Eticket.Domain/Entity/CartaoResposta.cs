using System;

namespace Eticket.Domain.Entity
{
    public class CartaoResposta
    {
        public Guid CartaoRespostaGuid { get; set; }
        public int TipoOperacao { get; set; }
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
        public int QuantidadeParcela { get; set; }
        public string CodigoSat { get; set; }
        public DateTime DataTransacao { get; set; }
        public DateTime DataComprovante { get; set; }
        public string Operadora { get; set; }
        public int TipoTransacao { get; set; }
        public string Comprovante { get; set; }
        public string Menssagem { get; set; }
    }
}