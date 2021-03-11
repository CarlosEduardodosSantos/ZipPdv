using System;
using System.Linq;
using Eticket.Application.Interface;

namespace Zip.EticketSub.Wizard
{
    public partial class frm3 : FormBase
    {
        public frm3()
        {
            InitializeComponent();

        }
        public override bool Concluir()
        {
            try
            {
                if (!string.IsNullOrEmpty(cbLoja.Text))
                    GetValueApp.AddOrUpdateAppSettings("Loja", cbLoja.Text);
                if (!string.IsNullOrEmpty(cbPdv.Text))
                    GetValueApp.AddOrUpdateAppSettings("Pdv", cbPdv.Text);
                if (!string.IsNullOrEmpty(cbVendedor.Text))
                    GetValueApp.AddOrUpdateAppSettings("Vendedor", ((int)cbVendedor.SelectedValue).ToString());

                if (!string.IsNullOrEmpty(txtUsuario.Text))
                    GetValueApp.AddOrUpdateAppSettings("Username", txtUsuario.Text);
                if (!string.IsNullOrEmpty(txtSenha.Text))
                    GetValueApp.AddOrUpdateAppSettings("Password", txtSenha.Text);
                if (!string.IsNullOrEmpty(txtMerchantId.Text))
                    GetValueApp.AddOrUpdateAppSettings("MerchantId", txtMerchantId.Text);

              
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void frm3_Load(object sender, EventArgs e)
        {
            using (var vendedorRepository = Program.Container.GetInstance<IUsuarioAppService>())
            {
                var vendedores = vendedorRepository.GetAll().ToList();

                cbVendedor.DataSource = vendedores;
                cbVendedor.ValueMember = "VendedorId";
                cbVendedor.DisplayMember = "Nome";
            }


            var loja = GetValueApp.GetValue<int>("Loja");
            var pdv = GetValueApp.GetValue<int>("Pdv");
            var vendedorId = GetValueApp.GetValue<int>("Vendedor");
            var username = GetValueApp.GetValue<string>("Username");
            var password = GetValueApp.GetValue<string>("Password");
            var merchantId = GetValueApp.GetValue<string>("MerchantId");
            var autoConfirma = GetValueApp.GetValue<bool>("AutoConfirma");

            cbLoja.SelectedIndex = loja-1;
            cbPdv.SelectedIndex = pdv-1;
            cbVendedor.SelectedValue = vendedorId;
            txtUsuario.Text = username;
            txtSenha.Text = password;
            txtMerchantId.Text = merchantId;
            
        }
    }
}
