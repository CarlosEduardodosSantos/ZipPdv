using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Cadastro;
using Zip.Pdv.Cadastro.Produto;
using Zip.Pdv.Cadastro.Venda;
using Zip.Pdv.Component;
using Zip.Pdv.Component.CustomTab;

namespace Zip.Pdv.Page
{
    public partial class PageVendaAdministracao : PageBase
    {
        private CadastroBase _cadastroBase;
        private List<VendaViewModel> _vendas;
        private VendaViewModel _venda;
        private readonly IVendaAppService _vendaAppService;

        public PageVendaAdministracao(VendaViewModel venda)
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;
            _vendaAppService = Program.Container.GetInstance<IVendaAppService>();
            _venda = venda;
            OpenDetalheVenda(_venda);
        }
        public PageVendaAdministracao()
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;
            _vendaAppService = Program.Container.GetInstance<IVendaAppService>();
        }

        private void btnPesquisaVenda_Click(object sender, EventArgs e)
        {
            _vendas = new List<VendaViewModel>();

            if (!string.IsNullOrEmpty(txtVendaId.Text))
            {
                var vendaId = 0;
                int.TryParse(txtVendaId.Text, out vendaId);
                var venda = _vendaAppService.ObterPorId(vendaId);

                if (venda == null)
                    TouchMessageBox.Show("Venda não encontrada.\nVerifique o numero e tente novamente.", "Administração de venda", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    _vendas.Add(venda);
            }
            else
            {
                if (dtpInicio.Checked || dtpFinal.Checked)
                {
                    var dataInicio = dtpInicio.Checked ? dtpInicio.Value.Date : DateTime.Now.Date;
                    var dataFinal = dtpFinal.Checked ? dtpFinal.Value.Date : DateTime.Now.Date;
                    _vendas = _vendaAppService.ObterPorData(dataInicio, dataFinal).ToList();
                }
                else
                    _vendas = _vendaAppService.ObterEntregaPendentes().ToList();
            }
            
            CarregaGridPesquisa();
        }

        private void CarregaGridPesquisa()
        {
            dgvVendas.AutoGenerateColumns = false;
            dgvVendas.DataSource = _vendas;
        }

        private void txtVendaId_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)return;

            btnPesquisaVenda.PerformClick();
        }

        private void dgvVendas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVendas.SelectedRows.Count <= 0) return;
            var index = dgvVendas.SelectedRows[0].Index;
            
            _venda = _vendas[index];

            OpenDetalheVenda(_venda);
        }

        private void OpenDetalheVenda(VendaViewModel vendaView)
        {            
            var headerPage = vendaView.IsDelivery ? $"Televenda [{vendaView.VendaId}]" : $"Venda Balção [{vendaView.VendaId}]";
            var page = new TabPageEx(headerPage);
            //page.Disposed += page_Disposed;
            page.AutoScroll = true;
            page.ImageIndex = 8;

            page.Name = $"{vendaView.VendaId}";
            page.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            int indexPage = IndexPage(page);
            if (indexPage == -1)
            {
                _cadastroBase = new VendaDetalhe() { Dock = DockStyle.Fill };
                _cadastroBase.ClassToObjeto(vendaView);
                page.Controls.Add(_cadastroBase);
                krbTabControl1.TabPages.Add(page);
                krbTabControl1.SelectedTab = page;
            }
            else
                krbTabControl1.SelectedIndex = indexPage;
        }

        public int IndexPage(TabPage page)
        {
            int index = -1;

            for (var i = 0; i < krbTabControl1.TabPages.Count; i++)
            {
                if (krbTabControl1.TabPages[i].Name != page.Name) continue;
                index = i;
                break;
            }

            return index;
        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
