using System;
using System.Runtime.InteropServices;
using SAT.Interface;
using Zip.Sat;

namespace SAT.Modelo
{
    public class Emulador : ISat
    {
        #region Declaracao dll
        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConsultarStatusOperacional(int sessao, string cod);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr EnviarDadosVenda(int sessao, string cod, string dados);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CancelarUltimaVenda(int sessao, string cod, string chave, string dadoscancel);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TesteFimAFim(int sessao, string cod, string dados);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConsultarSAT(int sessao);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConsultarNumeroSessao(int sessao, string cod, int sessao_a_ser_consultada);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AtivarSAT(int sessao, int tipoCert, string cod_Ativacao, string cnpj, int uf);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ComunicarCertificadoICPBRASIL(int sessao, string cod, string csr);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ConfigurarInterfaceDeRede(int sessao, string cod, string xmlConfig);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AssociarAssinatura(int sessao, string cod, string cnpj, string sign_cnpj);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr DesbloquearSAT(int sessao, string cod_ativacao);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr BloquearSAT(int sessao, string cod_ativacao);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TrocarCodigoDeAtivacao(int sessao, string cod_ativacao, int opcao, string nova_senha, string conf_senha);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ExtrairLogs(int sessao, string cod_ativacao);

        [DllImport("SAT.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AtualizarSoftwareSAT(int sessao, string cod_ativacao);
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
            return Marshal.PtrToStringAnsi(EnviarDadosVenda(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, XML));
        }
        string ISat.ICancelarUltimaVenda(int NumeroSessao, string chave, string cnpjSoftwareHouse, string cnpjCpfDestinatario, string NumeroCaixa, string SignAC)
        {
            string XmlCancelamento = "";

            if (cnpjCpfDestinatario.Trim().Length == 0)
                XmlCancelamento = "<?xml version='1.0' encoding='UTF-8'?><CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest> </dest><total/><infAdic/></infCFe></CFeCanc>";
            else if (cnpjCpfDestinatario.Trim().Length == 14)
                XmlCancelamento = "<?xml version='1.0' encoding='UTF-8'?><CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest><CNPJ>" + cnpjCpfDestinatario + "</CNPJ></dest><total/><infAdic/></infCFe></CFeCanc>";
            else
                XmlCancelamento = "<?xml version='1.0' encoding='UTF-8'?><CFeCanc><infCFe chCanc=\"CFe" + chave + "\"><ide><CNPJ>" + cnpjSoftwareHouse + "</CNPJ><signAC>" + SignAC + "</signAC><numeroCaixa>" + NumeroCaixa + "</numeroCaixa></ide><emit/><dest><CPF>" + cnpjCpfDestinatario + "</CPF></dest><total/><infAdic/></infCFe></CFeCanc>";

            return Marshal.PtrToStringAnsi(CancelarUltimaVenda(NumeroSessao, Global.ConfiguracaoInicial.SoftwareHouseChaveAtivacao, "CFe" + chave, XmlCancelamento));
        }
    }
}
