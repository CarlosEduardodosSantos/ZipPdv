using Eticket.Application.ViewModels;
using Zip.Pdv.NFce;

namespace Zip.Pdv
{
    public static class OperacoeFiscal
    {
        public static RetornoSatViewModel ImprimeSat(VendaViewModel vendaView)
        {
            using (var formSat = new Sat.FrmSolicitaSat(vendaView))
            {
                formSat.ShowDialog();
                return formSat.RetornoSatView;
            }
        }

        
        public static void ImprimeNfce(VendaViewModel vendaView)
        {
            using (var formNfce = new FrmSolicitaNfce(vendaView.VendaId))
            {
                formNfce.ShowDialog();
            }


        }
    }
}