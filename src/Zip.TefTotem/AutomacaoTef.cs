using System.IO;
using System.Text;
using System.Threading;
using Eticket.Application.ViewModels;

namespace Zip.TefTotem
{
    public class AutomacaoTef
    {
        public static CartaoRespostaViewModel AcionaTef(CartaoTipoOperacaoEnumView tipoOperacao, decimal valorReceber, string numeroDocumento, 
            EspecieCartaoTipoEnumView tipoCartao, string pdv, string codLoja, string cnpj)
        {
            using (var form = new FormProcessandoGlobal(tipoOperacao, valorReceber, numeroDocumento, tipoCartao, pdv, codLoja, cnpj))
            {

                form.ShowDialog();
                return form.CartaoRespostaView;
            }
        }

        

        
        public static CartaoRespostaViewModel Cancelamento(CartaoRespostaViewModel cartaoResposta, string pdv, string codLoja, string cnpj)
        {
            using (var form = new FormCancelamentoGlobal(cartaoResposta, pdv, codLoja, cnpj))
            {
                form.ShowDialog();
                return form.CartaoRespostaView;
            }
        }
    }
}