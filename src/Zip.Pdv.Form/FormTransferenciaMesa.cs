using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
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
    public partial class FormTransferenciaMesa : Form
    {
        private CadMesasViewModel _mesa;
        private readonly ICadMesasAppService _CadMesasAppService;
        private readonly IOpMesa1AppService _OpMesa1AppService;
        public FormTransferenciaMesa(CadMesasViewModel mesa)
        {
            _mesa = mesa;
            _CadMesasAppService = Program.Container.GetInstance<ICadMesasAppService>();
            _OpMesa1AppService = Program.Container.GetInstance<IOpMesa1AppService>();
            InitializeComponent();
        }

        private void FormTransferenciaMesa_Load(object sender, EventArgs e)
        {
            var mesas = _CadMesasAppService.ObterMesasDisponiveis().ToList();
            cmbMesas.DataSource = new BindingSource(mesas, null);
            cmbMesas.DisplayMember = "IdMesa";
            cmbMesas.ValueMember = "IdMesa";
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            TransfereMesa(Convert.ToInt32(cmbMesas.SelectedValue.ToString()));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TransfereMesa(int id)
        {
            //Reseta mesa atual
            var mesa = _mesa;
            mesa.Status = 1;
            _CadMesasAppService.AlterarStatusMesa(mesa);
            //Transfere Status pra mesa disponivel
            mesa.IdMesa = id;
            mesa.Status = 3;
            _CadMesasAppService.AlterarStatusMesa(mesa);
            _CadMesasAppService.IncluirOpMesa1(mesa);
            //Transfere id da mesa na opMesa1
            var opmesa = new OpMesa1ViewModel();
            opmesa.IdMesa = id;
            opmesa.IdopMesa1 = mesa.OpMesa1Atual;
            _OpMesa1AppService.Transferir(opmesa);
        }
    }
}
