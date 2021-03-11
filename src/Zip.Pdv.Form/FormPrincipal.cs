using System;
using System.Drawing;
using System.Windows.Forms;
using Zip.Pdv.Component;
using Zip.Pdv.Page;

namespace Zip.Pdv
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            timer1.Interval = 9000;
            timer1.Start();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            Program.Usuario = FormLogin.EfetuarLogin();
            if (Program.Usuario == null)
            {
                Close();
                return;
            }

            lbUsuarioNome.Text = Program.Usuario.Nome;
            lbEmpresa.Text = Program.EmpresaView.Fantasia;

            splitBtnConfigure.AddDropDownItemAndHandle("Caixa Abertura", btnCaixaAbertura_Click);
            splitBtnConfigure.AddDropDownItemAndHandle("Caixa Fechamento", btnCaixaFechamento_Click);
            splitBtnConfigure.AddDropDownItemAndHandle("Caixa Movimentacao", btnCaixaMovimentacao_Click);
            splitBtnConfigure.AddDropDownItemAndHandle("Caixa Lancamentos", btnCaixaMovimentacao_Click);
            //splitBtnConfigure.AddDropDownItemAndHandle("Configurações", btnConfiguracao_Click);

            OpenMenu();

        }

        private void btnCaixaMovimentacao_Click(object sender, EventArgs e)
        {
            var page = new PageCaixaMovimentacao();
            OpePage(page);
        }

        private void btnConfiguracao_Click(object sender, EventArgs e)
        {
            var page = new PageConfiguracoes();
            OpePage(page);
        }

        private void OpePage(PageBase page)
        {
            using (var form = new FormBase(page))
            {
                form.Width = panelPages.Width - 100;
                form.Height = panelPages.Height - 50;
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point()
                {
                    X = panelPages.Location.X + 50,
                    Y = panelPages.Location.Y + 25
                };
                form.ShowDialog();
            }
        }

        private void btnCaixaAbertura_Click(object sender, EventArgs e)
        {
            if (Program.CaixaView != null)
            {
                TouchMessageBox.Show("Já exite um caixa aberto para esse PDV.", "Abertura de Caixa",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var form = new FormAbrirCaixa())
            {
                form.ShowDialog();
            }
            btnVoltar.PerformClick();
        }
        private void btnCaixaFechamento_Click(object sender, EventArgs e)
        {
            if (Program.CaixaView == null)
            {
                if (TouchMessageBox.Show("Não exite caixa aberto para esse PDV.\nGostaria abrir o caixa?",
                        "Abertura de Caixa",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    btnCaixaAbertura_Click(sender, e);

                return;
            }

            using (var form = new FormCaixaFechamento())
            {
                form.ShowDialog();
            }
            btnVoltar.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            OpenMenu();
        }

        private void OpenMenu()
        {

            if (FormPdv.Instance.VendaView?.VendaItens?.Count > 0)
            {
                TouchMessageBox.Show("A venda não foi finalizada!\nÉ necessário que conclua ou cancele essa venda.",
                    "PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnVoltar.Visible = false;
            if (!panelPages.Controls.Contains(PageMenu.Instance))
            {
                PageMenu.Instance.Dock = DockStyle.Fill;
                panelPages.Controls.Add(PageMenu.Instance);
                PageMenu.Instance.SelectItem += Instance_SelectItem;
                PageMenu.Instance.BringToFront();
            }
            else
                PageMenu.Instance.BringToFront();

        }

        private void Instance_SelectItem(object sender, EventArgs e)
        {
            var evento = (Button) sender;

            if (evento.Tag == "FormPdv")
            {
                OpenPdv();
            }
            else if (evento.Tag == "ProdutoCad")
            {
                var page = new PageProdutoCadastro();
                OpePage(page);
            }

            
        }

        private void OpenPdv()
        {
            if (Program.CaixaView == null)
            {
                if (TouchMessageBox.Show("Caixa fechado\nDeseja abrir?", "Caixa", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var form = new FormAbrirCaixa())
                    {
                        form.ShowDialog();
                        if (Program.CaixaView == null)
                            return;
                    }
                }
                else
                {
                    return;
                }
            }

            btnVoltar.Visible = true;
            if (!panelPages.Controls.Contains(FormPdv.Instance))
            {
                FormPdv.Instance.Dock = DockStyle.Fill;
                panelPages.Controls.Add(FormPdv.Instance);
                FormPdv.Instance.BringToFront();
            }
            else
                FormPdv.Instance.BringToFront();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            CarregaIfood();
        }

        private void CarregaIfood()
        {
            splitBtnIfood.Text = "0";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            var page = new PageDelivery();
            OpePage(page);
        }
    }
}
