namespace Eticket.Application.ViewModels
{
    public class EmpresaConfiguracaoViewModel
    {
        public int EmpresaConfiguracaoId { get; set; }
        public int EmpresaId { get; set; }
        public string CertificadoDigital { get; set; }
        public string PerfilGeracaoSped { get; set; }
        public bool SimplesNacionalPermiteCreditoIcms { get; set; }
        public bool GeradorIcmsSubstituto { get; set; }
        public decimal SimplesNacionalIcmsAliquota { get; set; }
        public decimal ContribuicaoSocialAliquota { get; set; }
        public decimal ImpostoRendaAliquota { get; set; }
        public decimal CustoFixoPercentual { get; set; }
        public decimal OutrasDespesasPercentual { get; set; }
        public int CodigoRegimeTributario { get; set; }
    }
}