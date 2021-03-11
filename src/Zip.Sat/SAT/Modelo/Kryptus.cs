using System;
using System.Runtime.InteropServices;
using SAT.Interface;
using Zip.Sat;

namespace SAT.Modelo
{
    public class Kryptus : ISat
    {
        #region Declaracao dll
        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ConsultarStatusOperacional(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr AtivarSAT(int numeroSessao, int subComando, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
        [MarshalAs(UnmanagedType.LPStr)] string CNPJ, int cUF);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ComunicarCertificadoICPBRASIL(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
        [MarshalAs(UnmanagedType.LPStr)] string certificado);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr EnviarDadosVenda(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
        [MarshalAs(UnmanagedType.LPStr)] string dadosVenda);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CancelarUltimaVenda(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao, [MarshalAs(UnmanagedType.LPStr)] string chave, [MarshalAs(UnmanagedType.LPStr)] string dadosCancelamento);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ConsultarSAT(int numeroSessao);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr TesteFimAFim(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
        [MarshalAs(UnmanagedType.LPStr)] string dadosVenda);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ConsultarNumeroSessao(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao, int cNumeroDeSessao);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ConfigurarInterfaceDeRede(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
        [MarshalAs(UnmanagedType.LPStr)] string dadosConfiguracao);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr AssociarAssinatura(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao, [MarshalAs(UnmanagedType.LPStr)] string CNPJvalue, [MarshalAs(UnmanagedType.LPStr)] string assinaturaCNPJs);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr AtualizarSoftwareSAT(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ExtrairLogs(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

        [DllImport("Kryptus-SAT-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr BloquearSAT(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

        [DllImport("Kryptus-SAT-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr DesbloquearSAT(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

        [DllImport("Kryptus-SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr TrocarCodigoDeAtivacao(int numeroSessao, [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao, [MarshalAs(UnmanagedType.LPStr)] string opcao, [MarshalAs(UnmanagedType.LPStr)] string novoCodigo, [MarshalAs(UnmanagedType.LPStr)] string confNovoCodigo);
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
            string ret = Marshal.PtrToStringAnsi(ConsultarNumeroSessao(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, NumeroSessaoConsulta));
            return ret;
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