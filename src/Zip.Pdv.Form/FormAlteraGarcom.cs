using Eticket.Application.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv
{
    public partial class FormAlteraGarcom : Form
    {
        private readonly ICadGarcomAppService _CadGarcomAppService;
        public string garcom { get; set; }
        int garcomId;
        public FormAlteraGarcom(int garcomId = 0)
        {
            _CadGarcomAppService = Program.Container.GetInstance<ICadGarcomAppService>();
            InitializeComponent();
            this.garcomId = garcomId;
        }

        private void FormAlteraGarcom_Load(object sender, EventArgs e)
        {
            var garcons = _CadGarcomAppService.ObterGarcons().ToList();

            cmbGarcom.DataSource = new BindingSource(garcons, null);
            cmbGarcom.DisplayMember = "Descricao";
            cmbGarcom.ValueMember = "IdGarcom";
            cmbGarcom.SelectedIndex = garcomId - 1;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            garcom = cmbGarcom.SelectedValue.ToString();
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
    
}
