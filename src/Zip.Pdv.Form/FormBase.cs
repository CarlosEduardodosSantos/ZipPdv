using System.Windows.Forms;
using Zip.Pdv.Page;

namespace Zip.Pdv
{
    public partial class FormBase : Form
    {
        public FormBase(PageBase page)
        {
            InitializeComponent();
            this.Width = page.Width;
            this.Height = page.Height;
            page.Dock = DockStyle.Fill;
            page.CloseForm += Page_CloseForm;
            panelPrincipal.Controls.Add(page);
        }

        private void Page_CloseForm(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
