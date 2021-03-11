using System;
using System.Runtime.InteropServices;
using SAT.Interface;
using Zip.Sat;

namespace SAT.Modelo
{
    public class Dimep : ISat
    {
        #region Declaracao dll
        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarStatusOperacional(int sessao, string cod);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnviarDadosVenda(int sessao, string cod, string dados);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CancelarUltimaVenda(int sessao, string cod, string chave, string dadoscancel);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TesteFimAFim(int sessao, string cod, string dados);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarSAT(int sessao);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConsultarNumeroSessao(int sessao, string cod, int sessao_a_ser_consultada);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AtivarSAT(int sessao, int tipoCert, string cod_Ativacao, string cnpj, int uf);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ComunicarCertificadoICPBRASIL(int sessao, string cod, string csr);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ConfigurarInterfaceDeRede(int sessao, string cod, string xmlConfig);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AssociarAssinatura(int sessao, string cod, string cnpj, string sign_cnpj);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr DesbloquearSAT(int sessao, string cod_ativacao);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr BloquearSAT(int sessao, string cod_ativacao);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TrocarCodigoDeAtivacao(int sessao, string cod_ativacao, int opcao, string nova_senha, string conf_senha);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ExtrairLogs(int sessao, string cod_ativacao);

        [DllImport("DIMEP-SAT.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AtualizarSoftwareSAT(int sessao, string cod_ativacao);

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