using System;

namespace Eticket.Domain.Entity
{
    public class CartaoRequisicao
    {
        public Guid CartaoRequisicaoGuid { get; set; }
        public int TipoOperacao { get; set; }
        public DateTime DataHora { get; set; }
        public int Requisicao { get; set; }
        public string Vinculado { get; set; }
        public decimal Valor { get; set; }
        public string CodigoNsu { get; set; }
        public bool Parcelada { get; set; }
        public bool ParceladaLoja { get; set; }
        public int QuantidadeParcela { get; set; }
        public string EmpresaCnpj { get; set; }
    }
}