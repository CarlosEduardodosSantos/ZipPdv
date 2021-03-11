using System.Windows.Forms;

namespace Zip.EticketSub.Wizard
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
        }

        public virtual bool Concluir()
        {
            return true;
        }
    }
}
