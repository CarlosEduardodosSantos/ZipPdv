namespace Eticket.Domain.Entity
{
    public class ProdutoTributacao
    {
        public int ProdutoTributacaoId { get; set; }
        public int ProdutoId { get; set; }
        public string NcmCodigo { get; set; }
        public decimal NcmAliquotaFederalNacional { get; set; }
        public decimal NcmAliquotaEstadual { get; set; }
        public string PisCofinsTipo { get; set; }
        public string PisCofinsCstEntrada { get; set; }
        public string PisCofinsCstSaida { get; set; }
        public decimal PisAliquota { get; set; }
        public decimal CofinsAliquota { get; set; }
        public string CcsApurada { get; set; }
        public string TipoServico { get; set; }
        public string Origem { get; set; }
        public string IcmsCstEntrada { get; set; }
        public string IcmsCstSaida { get; set; }
        public string IcmsTipoEntrada { get; set; }
        public string IcmsTipoSaida { get; set; }
        public decimal IcmsAliquotaEntrada { get; set; }
        public decimal IcmsAliquotaEntradaReducao { get; set; }
        public decimal IcmsAliquotaIva { get; set; }
        public decimal IcmsAliquotaSaida { get; set; }
        public decimal IcmsAliquotaSaidaReducao { get; set; }
        public decimal IcmsAliquotaSaidaFinal { get; set; }
        public bool IvaAjustadoCalcular { get; set; }
        public string CestCodigo { get; set; }
        public string Cfop { get; set; }
    }
}