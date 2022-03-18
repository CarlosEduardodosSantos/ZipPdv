using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip.TefTotem.Interface
{
    public  interface ITefTotem
    {
        string IIniciaFuncaoMCInterativo(int iComando, string sCnpjCliente, int iParcela, string sCupom, string sValor, string sNsu, string sData, string sNumeroPDV, string sCodigoLoja, int sTipoComunicacao);
        string IAguardaFuncaoMCInterativo();
        string IContinuaFuncaoMCInterativo(string sInformaca);
        string IFinalizaFuncaoMCInterativo(int iComando, string sCnpjCliente, int iParcela, string sCupom, string sValor, string sNsu, string sData, string sNumeroPDV, string sCodigoLoja, int sTipoComunicacao);
        string ICancelarFluxoMCInterativo();

    }
}
