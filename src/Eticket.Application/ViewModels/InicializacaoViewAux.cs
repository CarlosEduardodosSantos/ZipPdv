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
    }
}