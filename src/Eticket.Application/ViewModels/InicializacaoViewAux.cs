namespace Eticket.Application.ViewModels
{
    public class InicializacaoViewAux
    {
        public TipoImpressaoViewEnum TipoImpressao { get; set; }
        public string CaminhoEssencial { get; set; }
        public string CaminhoTef { get; set; }
        public decimal ValorFrete { get; set; }
        public bool HabSenhaExcluirItem { get; set; }
        public string SenhaExcluirItem { get; set; }
        public decimal DescontoMaximo { get; set; }
        public int EspeciePagamentoDinheiroId { get; set; }
        public int CedenteId { get; set; }
        public bool ModoPdv { get; set; }
        public bool HabSenhaPager { get; set; }
        public bool BalancaDigitada { get; set; }
        public string CodigoLoja { get; set; }
        public string Cnpj { get; set; }
        public string Pdv { get; set; }
        public string PdvTef { get; set; }
        public int RestauranteId { get; set; }
    }
}