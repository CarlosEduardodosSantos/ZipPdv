using Eticket.Application.Interface;
using System;
using System.Drawing;
using System.Linq;
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

            using (var usuarioAppService = Program.Container.GetInstance<IUsuarioAppService>())
            {
                var dirAdmVenda = usuarioAppService.VerificaPrivilegio("AdmVendas1", Program.Usuario.UsuarioId);
                var dirCaixaMov = usuarioAppService.VerificaPrivilegio("CaixaGerencial", Program.Usuario.UsuarioId);

                splitBtnConfigure.AddDropDownItemAndHandle("Caixa Abertura", btnCaixaAbertura_Click);
                splitBtnConfigure.AddDropDownItemAndHandle("Caixa Fechamento", btnCaixaFechamento_Click);
                if (dirCaixaMov)
                    splitBtnConfigure.AddDropDownItemAndHandle("Caixa Movimentacao", btnCaixaMovimentacao_Click);
                //splitBtnConfigure.AddDropDownItemAndHandle("Caixa Lancamentos", btnCaixaMovimentacao_Click);
                //splitBtnConfigure.AddDropDownItemAndHandle("Configurações", btnConfiguracao_Click);
                if (dirAdmVenda)
                    splitBtnConfigure.AddDropDownItemAndHandle("Venda ADM", btnVendaAdm_Click);

            }



            OpenMenu();
            if (Program.InicializacaoViewAux.ModoPdv)
            {
                //btnDelivery.Visible = false;
                //splitBtnIfood.Visible = false;
                //splitBtnConfigure.Visible = false;

                if (Program.CaixaView == null)
                {
                    var result = TouchMessageBox.Show("Caixa fechado\nDeseja abrir?", "Caixa", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
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

                switch (Program.TipoPdv)
                {
                    case Eticket.Application.ViewModels.ModoPdvEnumView.Pedido:
                        OpenPdv();
                        break;
                    case Eticket.Application.ViewModels.ModoPdvEnumView.TotemMenu:
                        OpenMenuTotem();
                        //OpenPdvToten();
                        break;
                    default:
                        OpenPdv();
                        break;
                }
            }

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
        private void btnVendaAdm_Click(object sender, EventArgs e)
        {
            var page = new PageVendaAdministracao();
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

        private void OpenTotemPedido()
        {
            btnVoltar.Visible = true;
            btnVoltar.Visible = false;
            panel26.Visible = false;
            btnDelivery.Visible = false;
            splitBtnConfigure.Visible = false;

            if (!panelPages.Controls.Contains(FormPdvToten.Instance))
            {
                FormPdvToten.Instance.Dock = DockStyle.Fill;
                panelPages.Controls.Add(FormPdvToten.Instance);
                FormPdvToten.Instance.BringToFront();
            }
            else
                FormPdv.Instance.BringToFront();

        }
        private void OpenTotemPagamento()
        {
            btnVoltar.Visible = true;
            btnVoltar.Visible = false;
            panel26.Visible = false;
            btnDelivery.Visible = false;
            splitBtnConfigure.Visible = false;

            if (!panelPages.Controls.Contains(PagePagamento.Instance))
            {
                PagePagamento.Instance.Dock = DockStyle.Fill;
                panelPages.Controls.Add(PagePagamento.Instance);
                PagePagamento.Instance.BringToFront();
            }
            else
                PagePagamento.Instance.BringToFront();
        }
        private void OpenMenuTotem()
        {
            btnVoltar.Visible = true;
            btnRetira.Visible = false;
            btnVoltar.Visible = false;
            panel26.Visible = false;
            btnDelivery.Visible = false;
            splitBtnConfigure.Visible = false;


            if (!panelPages.Controls.Contains(PagePrincipalTotem.Instance))
            {
                PagePrincipalTotem.Instance.Dock = DockStyle.Fill;
                panelPages.Controls.Add(PagePrincipalTotem.Instance);
                PagePrincipalTotem.Instance.SelectItem += Instance_SelectItem;
                PagePrincipalTotem.Instance.BringToFront();
            }
            else
                PagePrincipalTotem.Instance.BringToFront();

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
            using (var vendaAppService = Program.Container.GetInstance<IVendaAppService>())
            {
                var televendaPendentes = vendaAppService.ObterEntregaPendentes().Count();
                if (televendaPendentes > 0)
                {
                    TouchMessageBox.Show("Existe televendas pendente de saída/retorno\nVerifique as pendências e tente novamente, efetuar o fechamento do caixa.", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDelivery.PerformClick();
                    return;
                }
            }

            using (var form = new FormCaixaFechamento())
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                    Application.Restart();
            }

            //btnVoltar.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (panelPages.Controls.Contains(FormPdv.Instance))
            {
                if (FormPdv.Instance.VendaView.VendaItens.Count > 0)
                {
                    TouchMessageBox.Show("Exite uma venda em andamento!\nFinalize a venda para sair do sistema.", "Operação proibida", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    return;
                }
            }
            var senha = FormSolicitaSenha.Instace();
            if (senha != Program.Usuario.Senha)
            {
                TouchMessageBox.Show("Senha incorreta.\nVerifique e tente novamente.", "Operação proibida", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            if (Program.TipoPdv == Eticket.Application.ViewModels.ModoPdvEnumView.TotemMenu)
            {
                btnVoltar.Visible = false;
                timer1.Stop();

                OpenMenuTotem();
            }
            else if (!Program.InicializacaoViewAux.ModoPdv)
                OpenMenu();
            else
            {
                TouchMessageBox.Show("Função desabilitada para esse PDV.\nConsulte o administrador.",
                    "PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            var evento = (Button)sender;

            if (evento.Tag == "FormPdv")
            {
                if (Program.TipoPdv == Eticket.Application.ViewModels.ModoPdvEnumView.Pedido)
                    OpenPdv();
            }
            else if (evento.Tag == "FormPdvToten")
            {
                if (Program.TipoPdv == Eticket.Application.ViewModels.ModoPdvEnumView.TotemMenu)
                {
                    btnVoltar.Visible = true;
                    OpenPdvToten();
                }


            }
            else if (evento.Tag == "ProdutoCad")
            {
                var page = new PageProdutoCadastro();
                OpePage(page);
            }
            else if (evento.Tag == "PagePagamento")
            {
                btnVoltar.Visible = true;
                OpenPagamentoToten();
            }


        }
        private void OpenPdv()
        {


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
        private void OpenPdvToten()
        {
            if (Program.CaixaView == null)
            {
                var result = TouchMessageBox.Show("Caixa fechado\nDeseja abrir?", "Caixa", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);

                if (result == DialogResult.OK)
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

            var page = new FormPdvToten();


            if (panelPages.Controls.Contains(page))
            {
                panelPages.Controls.Remove(page);
            }
            page.Dock = DockStyle.Fill;
            panelPages.Controls.Add(page);
            page.BringToFront();

        }

        private void OpenPagamentoToten()
        {
            if (Program.CaixaView == null)
            {
                var result = TouchMessageBox.Show("Caixa fechado\nProcure um funcionário para ajudar.", "Caixa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (panelPages.Controls.Contains(PagePagamento.Instance))
            {
                PagePagamento.Instance.Dispose();
            }

            var page = new PagePagamento();
            if (panelPages.Controls.Contains(page))
            {
                panelPages.Controls.Remove(page);
            }
            page.Dock = DockStyle.Fill;
            panelPages.Controls.Add(page);
            page.BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CarregaIfood();
        }

        private void CarregaIfood()
        {
            //splitBtnIfood.Text = "0";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            var page = new PageDelivery();
            OpePage(page);
        }

        private void lbUsuarioNome_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Program.IsFrete = !Program.IsFrete;
            lbUsuarioNome.ForeColor = Program.IsFrete ? Color.Black : Color.Red;
        }

        public static void IsFrete()
        {

        }

        private void splitBtnIfood_Click(object sender, EventArgs e)
        {
            var page = new PagePendencia();
            OpePage(page);
        }
    }
}
