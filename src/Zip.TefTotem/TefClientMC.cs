using System;

using System.Runtime.InteropServices;
using Zip.TefTotem.Interface;

namespace Zip.TefTotem
{
    public class TefClientMC : ITefTotem
    {
        #region Declaracao dll
        [DllImport("TefClientMC.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr IniciaFuncaoMCInterativo(int iComando, [MarshalAs(UnmanagedType.LPStr)] string sCnpjCliente, int iParcela, [MarshalAs(UnmanagedType.LPStr)] string sCupom, [MarshalAs(UnmanagedType.LPStr)] string sValor, [MarshalAs(UnmanagedType.LPStr)] string sNsu, [MarshalAs(UnmanagedType.LPStr)] string sData, [MarshalAs(UnmanagedType.LPStr)] string sNumeroPDV, [MarshalAs(UnmanagedType.LPStr)] string sCodigoLoja, int sTipoComunicacao);

        [DllImport("TefClientMC.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AguardaFuncaoMCInterativo();
        [DllImport("TefClientMC.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ContinuaFuncaoMCInterativo([MarshalAs(UnmanagedType.LPStr)]  string sInformaca);

        [DllImport("TefClientMC.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FinalizaFuncaoMCInterativo(int iComando, [MarshalAs(UnmanagedType.LPStr)] string sCnpjCliente, int iParcela, [MarshalAs(UnmanagedType.LPStr)] string sCupom, [MarshalAs(UnmanagedType.LPStr)] string sValor, [MarshalAs(UnmanagedType.LPStr)] string sNsu, [MarshalAs(UnmanagedType.LPStr)] string sData, [MarshalAs(UnmanagedType.LPStr)] string sNumeroPDV, [MarshalAs(UnmanagedType.LPStr)] string sCodigoLoja, int sTipoComunicacao);

        [DllImport("TefClientMC.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CancelarFluxoMCInterativo();

        [DllImport("TefClientMC.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ShellExecute(long hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, long nShowCmd);
        

        //Private Declare Function GetDesktopWindow Lib "user32" () As Long

        //private const conSwNormal = 1;




        #endregion
        public string IIniciaFuncaoMCInterativo(int iComando, string sCnpjCliente, int iParcela, string sCupom, string sValor, string sNsu, string sData, string sNumeroPDV, string sCodigoLoja, int sTipoComunicacao)
        {
            var result = Marshal.PtrToStringAnsi(IniciaFuncaoMCInterativo(iComando, sCnpjCliente, iParcela, sCupom, sValor, sNsu, sData, sNumeroPDV, sCodigoLoja, sTipoComunicacao));
            return string.IsNullOrEmpty(result) ? "0" : result;
            
        }

        public string IAguardaFuncaoMCInterativo()
        {
            return Marshal.PtrToStringAnsi(AguardaFuncaoMCInterativo());
        }

        public string IContinuaFuncaoMCInterativo(string sInformaca)
        {
            var result = Marshal.PtrToStringAnsi(ContinuaFuncaoMCInterativo(sInformaca));
            return string.IsNullOrEmpty(result) ? "0" : result;
        }

        public string IFinalizaFuncaoMCInterativo(int iComando, string sCnpjCliente, int iParcela, string sCupom, string sValor, string sNsu, string sData, string sNumeroPDV, string sCodigoLoja, int sTipoComunicacao)
        {
            string result = Marshal.PtrToStringAnsi(FinalizaFuncaoMCInterativo(iComando, sCnpjCliente, iParcela, sCupom, sValor, sNsu, sData, sNumeroPDV, sCodigoLoja, sTipoComunicacao));
            return string.IsNullOrEmpty(result) ? "0" : result;
        }

        public string ICancelarFluxoMCInterativo()
        {
            return Marshal.PtrToStringAnsi(CancelarFluxoMCInterativo());
        }
    }
}