using Eticket.Application.ViewModels;
using Zip.Utils;

namespace Zip.Pdv.Page
{
    public partial class PageConfiguracoes : PageBase
    {
        public PageConfiguracoes()
        {
            InitializeComponent();

            btnVoltar.Click += closeForm;

            cbModeloFiscal.DataSource = Conversoes.Listar(typeof(ModeloFiscalEnumView));
            cbModeloFiscal.DisplayMember = "Value";
            cbModeloFiscal.ValueMember = "Key";
            cbModeloFiscal.SelectedValue = -1;
        }
    }
}
