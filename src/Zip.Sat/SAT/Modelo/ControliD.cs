using System;
using System.Runtime.InteropServices;
using SAT.Interface;
using Zip.Sat;

namespace SAT.Modelo
{
    public class ControliD : ISat
    {
        #region Declaracao dll

        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ConsultarSAT(int numeroSessao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr AssociarAssinatura(int numeroSessao, string codigoAtivacao, string CNPJvalue, string assinaturaCNPJs);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr AtivarSAT(int numeroSessao, int subComando, string codigoAtivacao, string CNPJ, string cUF);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr AtualizarSoftwareSAT(int numeroSessao, string codigoDeAtivacao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BloquearSAT(int numeroSessao, string codigoAtivacao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CancelarUltimaVenda(int numeroSessao, string codigoAtivacao, string chave, string dadosCancelamento);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ConfigurarInterfaceDeRede(int numeroSessao, string codigoAtivacao, string dadosConfiguracao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ConsultarNumeroSessao(int numeroSessao, string codigoAtivacao, string cNumeroDeSessao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ConsultarStatusOperacional(int numeroSessao, string codigoAtivacao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DesbloquearSAT(int numeroSessao, string codigoAtivacao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr EnviarDadosVenda(int numeroSessao, string codigoAtivacao, string dadosVenda);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ExtrairLogs(int numeroSessao, string codigoAtivacao);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr TesteFimAFim(int numeroSessao, string codigoAtivacao, string dadosVenda);
        [DllImport("SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr TrocarCodigoDeAtivacao(int numeroSessao, string codigoAtivacao, string novoCodigo, string confNovoCodigo);

        #endregion

        string ISat.IConsultarSAT(int NumeroSessao)
        {
            string ret = Marshal.PtrToStringAnsi(ConsultarSAT(NumeroSessao));
            return ret;
        }
        string ISat.IConsultarStatusOperacional(int NumeroSessao)
        {
            string ret = Marshal.PtrToStringAnsi(ConsultarStatusOperacional(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao));
            return ret;
        }
        string ISat.IEnviarDadosVenda(int NumeroSessao, string XML)
        {
            string ret = Marshal.PtrToStringAnsi(EnviarDadosVenda(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, XML));
            return ret;
        }
        string ISat.IConsultarNumeroSessao(int NumeroSessao, int NumeroSessaoConsulta)
        {
            string ret = Marshal.PtrToStringAnsi(ConsultarNumeroSessao(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, NumeroSessaoConsulta.ToString()));
            return ret;
        }
        string ISat.ICancelarUltimaVenda(int NumeroSessao, string chave, string cnpjSoftwareHouse, string cnpjCpfDestinatario, string NumeroCaixa, string SignAC)
        {
            string XmlCancelamento = "";
            if (Global.ConfiguracaoInicial.SatLayoutVersao == "0.07" || cnpjCpfDestinatario.Trim().Length == 0)
                XmlCancelamento = "<CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest> </dest><total/><infAdic/></infCFe></CFeCanc>";
            else if (cnpjCpfDestinatario.Trim().Length == 14)
                XmlCancelamento = "<CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest><CNPJ>" + cnpjCpfDestinatario + "</CNPJ></dest><total/><infAdic/></infCFe></CFeCanc>";
            else
                XmlCancelamento = "<CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest><CPF>" + cnpjCpfDestinatario + "</CPF></dest><total/><infAdic/></infCFe></CFeCanc>";
            return Marshal.PtrToStringAnsi(CancelarUltimaVenda(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, "CFe" + chave, XmlCancelamento));
        }
    }
}