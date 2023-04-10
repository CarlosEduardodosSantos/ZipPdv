using Eticket.Application;
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
    public partial class FormAbreMesa : Form
    {
        private readonly ICadGarcomAppService _CadGarcomAppService;
        public string garcom { get; set; }
        public int pessoas { get; set; }

        public FormAbreMesa()
        {
            _CadGarcomAppService = Program.Container.GetInstance<ICadGarcomAppService>();
            InitializeComponent();
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            garcom = cmbGarcom.SelectedValue.ToString();
            pessoas = Convert.ToInt32(nmbPessoas.Value);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormAbreMesa_Load(object sender, EventArgs e)
        {

            nmbPessoas.Value = 1;
            var garcons = _CadGarcomAppService.ObterGarcons().ToList();
            foreach (var item in garcons)
            {
                cmbGarcom.Items.Add(item);
            }
            cmbGarcom.DataSource = new BindingSource(garcons, null);
            cmbGarcom.DisplayMember = "Descricao";
            cmbGarcom.ValueMember = "IdGarcom";
        }
    }
}
