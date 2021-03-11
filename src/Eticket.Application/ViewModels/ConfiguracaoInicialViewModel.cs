namespace Eticket.Application.ViewModels
{
    public class ConfiguracaoInicialViewModel
    {
        public ModeloFiscalEnumView ModeloFiscal { get; set; }
        public string ImpressoraFiscalPorta { get; set; }
        public int NumeroLinhasEntreCupom { get; set; }
        public string SoftwareHouseCnpj { get; set; }
        public string SoftwareHouseChaveAtivacao { get; set; }
        public int CaixaNumero { get; set; }
        public string SatServidor { get; set; }
        public int PortaServidor { get; set; }
        public int SatPorta { get; set; }
        public string SatImpressora { get; set; }
        public string SatTextoRodape { get; set; }
        public string SalvarArquivosEm { get; set; }
        public string SatMarca { get; set; }
        public string SatLayoutVersao { get; set; }
    }
}