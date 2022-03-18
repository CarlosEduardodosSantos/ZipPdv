using System.IO;
using System.Text;
using System.Threading;
using Eticket.Application.ViewModels;

namespace Zip.TefDial
{
    public class AutomacaoTef
    {
        public static CartaoRespostaViewModel AcionaTef(CartaoTipoOperacaoEnumView tipoOperacao, decimal valorReceber, string numeroDocumento, EspecieCartaoTipoEnumView tipoCartao)
        {
            using (var form = new FormProcessandoGlobal(tipoOperacao, valorReceber, numeroDocumento, tipoCartao))
            {

                form.ShowDialog();
                return form.CartaoRespostaView;
            }
        }

        public static bool TefAtivo()
        {
            var srvFile = new StringBuilder();
            srvFile.AppendLine($"000-000 = ATV");
            srvFile.AppendLine($"001-000 = 0"); //Indica o número de controle da solicitação que está sendo feita (IdPedido)
            srvFile.AppendLine("999-999 = 0"); //Finaliza

            File.WriteAllText($"{MultiPlus.DiretorioReq}/{MultiPlus.NameFileReq}", srvFile.ToString());

            var fileSts = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileRespTemp}";
            var fileAtivo = $"{MultiPlus.DiretorioResp}/{MultiPlus.NameFileAtivo}";
            Thread.Sleep(7000);

            bool a = true;
            bool esperou = false;
            while (a)
            {
                if (File.Exists(fileSts))
                {
                    if (File.Exists(fileAtivo))
                    {
                        File.Delete(fileSts);
                        File.Delete(fileAtivo);

                        return true;
                    }
                }
                else if (esperou)
                {
                    File.Delete(fileSts);
                    return false;
                }

                //Thread.Sleep(7000);
                esperou = true;
            }
            return false;

        }

        public static CartaoRespostaViewModel Cancelamento(int requisicao)
        {
            using (var form = new FormCancelamentoGlobal(requisicao))
            {
                form.ShowDialog();
                return form.CartaoRespostaView;
            }
        }
    }
}