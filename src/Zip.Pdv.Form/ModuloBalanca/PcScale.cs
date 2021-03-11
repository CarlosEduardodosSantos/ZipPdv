using System.Runtime.InteropServices;

namespace Zip.Pdv.ModuloBalanca
{
    public enum ModeloBalancas : int
    {
        Filizola_IDS = 0,
        RiceLake_IQ_Plus_810 = 1,
        Toledo_9091 = 2,
        Toledo_8132 = 3,
        Filizola_IDS_ID10000 = 4,
        Filizola_BP = 5,
        Filizola_MF = 6,
        Filizola_IDC = 7,
        Filizola_E = 8,
        Filizola_CS_Pluris = 9,
        Filizola_IDM = 10,
        Filizola_IDU = 11,
        TruTest_SR2000 = 12,
        Filizola_CI = 13,
        Micheletti_Comercial = 14,
        Micheletti_Industrial = 15,
        Alfa_3102C = 16,
        Saturno_SB_5000_SII = 17,
        MIC300_WT300 = 18,
        Flexar_LR22 = 19,
        Alfa_3101C = 20,
        Filizola_IDS_Contadora = 21,
        Lider_LD2052 = 22
    }
    public enum RetornoBalanca : int
    {
        Status = 0,
        PesoBruto = 1,
        Tara = 2,
        PesoLiquido = 3,
        Contador = 4,
        Codigo = 5,
        ValorUnitario = 6,
        ValorTotal = 7,
        NumeroCasasDecimais = 8,
        PesoMedioPorPecaEm_g = 9,
        DataFabricacao = 10,
        DataValidade = 11
    }
    public enum PortaCOM : int
    {
        COM1 = 1,
        COM2 = 2,
        COM3 = 3,
        COM4 = 4,
        COM5 = 5,
        COM6 = 6,
        COM7 = 7,
        COM8 = 8
    }
    public enum TipoBalanca : int
    {
        Serial = 0,
        Ethernet = 1
    }

    public class PcScale
    {
        #region Metodos de Configuração

        [DllImport("PcScale.dll")]
        public static extern void ConfiguraBalanca(
            int balanca,
            object Aplicativo);

        [DllImport("PcScale.dll")]
        public static extern void SalvaParametrosBalanca(
            int balanca,
            ModeloBalancas modelo,
            PortaCOM portaCom,
            int baudRate);

        [DllImport("PcScale.dll")]
        public static extern void SalvaParametrosBalanca(
            int balanca,
            ModeloBalancas modelo,
            PortaCOM portaCom,
            int baudRate,
            TipoBalanca tipo,
            string ip,
            int portaComunicacao);

        [DllImport("PcScale.dll")]
        public static extern void ObtemParametrosBalanca(
            int balanca,
            out ModeloBalancas modelo,
            out PortaCOM portaCom,
            out int baudRate);

        [DllImport("PcScale.dll")]
        public static extern void ObtemParametrosBalanca(
            int balanca,
            out ModeloBalancas modelo,
            out PortaCOM portaCom,
            out int baudRate,
            out TipoBalanca tipo,
            out string ip,
            out int portaComunicacao);
        #endregion

        #region Métodos de Leitura
        [DllImport("PcScale.dll")]
        public static extern void ObtemNomeBalanca(
            ModeloBalancas modelo,
            out string Nome);

        [DllImport("PcScale.dll")]
        public static extern bool InicializaLeitura(int balanca);

        [DllImport("PcScale.dll")]
        public static extern bool FinalizaLeitura(int balanca);

        [DllImport("PcScale.dll")]
        public static extern double ObtemInformacao(int balanca, RetornoBalanca campo);
        #endregion

        #region Métodos Especiais
        // Rotina para envio do preço/kg para a CS
        [DllImport("PcScale.dll")]
        public static extern bool EnviaPrecoCS(int balanca, double preco);

        // Rotina para verificar se a porta selecionada está também em outra balança
        [DllImport("PcScale.dll")]
        public static extern bool PortaJaExiste(PortaCOM porta, int balanca);
        #endregion

        #region Retorna Erro na DLL
        // Rotina que retorna a mesagem de erro
        [DllImport("PcScale.dll")]
        public static extern void ObtemMsgErro(out string msg);
        #endregion
    }
}
