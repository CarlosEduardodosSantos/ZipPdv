using System;
using System.Runtime.InteropServices;
using SAT.Interface;
using Zip.Sat;

namespace SAT.Modelo
{
    public class Gertec : ISat
    {
        #region Declaracao dll

        [DllImport("GERSAT.dll", EntryPoint = "AtivarSAT", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AtivarSAT(int numeroSessao, int subComando, string codigoDeAtivacao, string CNPJ, int cUF);

        [DllImport("GERSAT.dll", EntryPoint = "ComunicarCertificadoICPBRASIL", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ComunicarCertificadoICPBRASIL(int numeroSessao, string codigoDeAtivacao, string certificado);

        [DllImport("GERSAT.dll", EntryPoint = "EnviarDadosVenda", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr EnviarDadosVenda(int numeroSessao, string codigoDeAtivacao, string dadosVenda);

        [DllImport("GERSAT.dll", EntryPoint = "CancelarUltimaVenda", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CancelarUltimaVenda(int numeroSessao, string codigoDeAtivacao, string chave, string dadosCancelamento);

        [DllImport("GERSAT.dll", EntryPoint = "ConsultarSAT", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConsultarSAT(int numeroSessao);

        [DllImport("GERSAT.dll", EntryPoint = "TesteFimAFim", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TesteFimAFim(int numeroSessao, string codigoDeAtivacao, string dadosVenda);

        [DllImport("GERSAT.dll", EntryPoint = "ConsultarStatusOperacional", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ConsultarStatusOperacional(int numeroSessao, string codigoDeAtivacao);

        [DllImport("GERSAT.dll", EntryPoint = "ConsultarNumeroSessao", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConsultarNumeroSessao(int numeroSessao, string codigoDeAtivacao, int cNumeroDeSessao);

        [DllImport("GERSAT.dll", EntryPoint = "ConfigurarInterfaceDeRede", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConfigurarInterfaceDeRede(int numeroSessao, string codigoDeAtivacao, string dadosConfiguracao);

        [DllImport("GERSAT.dll", EntryPoint = "AssociarAssinatura", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AssociarAssinatura(int numeroSessao, string codigoDeAtivacao, string CNPJvalue, string assinaturaCNPJs);

        [DllImport("GERSAT.dll", EntryPoint = "ExtrairLogs", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ExtrairLogs(int numeroSessao, string codigoDeAtivacao);

        [DllImport("GERSAT.dll", EntryPoint = " BloquearSAT", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr BloquearSAT(int numeroSessao, string codigoDeAtivacao);

        [DllImport("GERSAT.dll", EntryPoint = "DesbloquearSAT", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr DesbloquearSAT(int numeroSessao, string codigoDeAtivacao);

        [DllImport("GERSAT.dll", EntryPoint = "TrocarCodigoDeAtivacao", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TrocarCodigoDeAtivacao(int numeroSessao, string codigoDeAtivacao, int opcao, string novoCodigo, string confNovoCodigo);

        [DllImport("GERSAT.dll", EntryPoint = "AtualizarSoftwareSAT", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AtualizarSoftwareSAT(int numeroSessao, string codigoDeAtivacao);

        [DllImport("GERSAT.dll", EntryPoint = "VersaoDllGerSAT", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr VersaoDllGerSAT();

        #endregion

        string ISat.IConsultarSAT(int NumeroSessao)
        {
            return Marshal.PtrToStringAnsi(ConsultarSAT(NumeroSessao));
        }
        string ISat.IConsultarStatusOperacional(int NumeroSessao)
        {
            return Marshal.PtrToStringAnsi(ConsultarStatusOperacional(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao));
        }
        string ISat.IConsultarNumeroSessao(int NumeroSessao, int NumeroSessaoConsulta)
        {
            return Marshal.PtrToStringAnsi(ConsultarNumeroSessao(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, NumeroSessaoConsulta));
        }
        string ISat.IEnviarDadosVenda(int NumeroSessao, string XML)
        {
            var a = Marshal.PtrToStringAnsi(EnviarDadosVenda(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, XML));
            return a;
        }
        string ISat.ICancelarUltimaVenda(int NumeroSessao, string chave, string cnpjSoftwareHouse, string cnpjCpfDestinatario, string NumeroCaixa, string SignAC)
        {
            string XmlCancelamento = "";
            if (Global.ConfiguracaoInicial.SatLayoutVersao == "0.07" || cnpjCpfDestinatario.Trim().Length == 0)
                XmlCancelamento = "<?xml version='1.0' encoding='UTF-8'?><CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest> </dest><total/><infAdic/></infCFe></CFeCanc>";
            else if (cnpjCpfDestinatario.Trim().Length == 14)
                XmlCancelamento = "<?xml version='1.0' encoding='UTF-8'?><CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest><CNPJ>" + cnpjCpfDestinatario + "</CNPJ></dest><total/><infAdic/></infCFe></CFeCanc>";
            else
                XmlCancelamento = "<?xml version='1.0' encoding='UTF-8'?><CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest><CPF>" + cnpjCpfDestinatario + "</CPF></dest><total/><infAdic/></infCFe></CFeCanc>";
            return Marshal.PtrToStringAnsi(CancelarUltimaVenda(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, "CFe" + chave, XmlCancelamento));
        }
    }
}