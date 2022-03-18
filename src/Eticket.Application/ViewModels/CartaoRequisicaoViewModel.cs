using System;

namespace Eticket.Application.ViewModels
{
    public class CartaoRequisicaoViewModel
    {
        public CartaoRequisicaoViewModel()
        {
            CartaoRequisicaoGuid = Guid.NewGuid();
            DataHora = DateTime.Now;
        }
        public Guid CartaoRequisicaoGuid { get; set; }
        public CartaoTipoOperacaoEnumView TipoOperacao { get; set; }
        public DateTime DataHora { get; set; }
        public int Requisicao { get; set; }
        public string Vinculado { get; set; }
        public decimal Valor { get; set; }
        public string CodigoNsu { get; set; }
        public bool Parcelada { get; set; }
        public bool ParceladaLoja { get; set; }
        public int QuantidadeParcela { get; set; }
        public string EmpresaCnpj { get; set; }
        public string Pdv { get; set; }
        public string CodigoLoja { get; set; }
        public EspecieCartaoTipoEnumView TipoCartao { get; set; }
    }
}