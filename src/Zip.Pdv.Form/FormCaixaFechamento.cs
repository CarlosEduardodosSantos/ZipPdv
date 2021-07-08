using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;
using Zip.Pdv.Fast;

namespace Zip.Pdv
{
    public partial class FormCaixaFechamento : Form
    {
        private decimal _valorReceber;
        public List<CaixaFechamentoViewModel> Pagamentos;
        private readonly ICaixaItemAppService _caixaItemAppService;
        private EspeciePagamentoViewModel _especiePagamento;
        private CaixaViewModel caixaView;
        public FormCaixaFechamento()
        {
            InitializeComponent();
            Pagamentos = new List<CaixaFechamentoViewModel>();
            _especiePagamento = new EspeciePagamentoViewModel();
            _caixaItemAppService = Program.Container.GetInstance<ICaixaItemAppService>();
             caixaView = Program.CaixaView;
        }
        private void FormCaixaFechamento_Load(object sender, System.EventArgs e)
        {
            CarregaEspecies();
            TotalizaPagamento();
        }
        private void CarregaEspecies()
        {

            using (var especieAppService = Program.Container.GetInstance<IEspeciePagamentoAppService>())
            {
                var especies = especieAppService.ObterTodos();
                flayoutGrupo.Controls.Clear();
                foreach (var especiePagamentoViewModel in especies)
                {
                    var btnEspecie = new EspecieGridViewItem();
                    btnEspecie.AdicionarEspecie(especiePagamentoViewModel);
                    btnEspecie.SelectItem += xButton1_Click;
                    btnEspecie.Width = 131;
                    flayoutGrupo.Controls.Add(btnEspecie);
                }

            }
        }
        private void TotalizaPagamento()
        {
            lbValorReceber.Text = _valorReceber.ToString("C2");

            var valorPago = Pagamentos.Sum(t => t.Valor);
            lbValorPago.Text = valorPago.ToString("C2");
            //var saldoPagar = (_valorReceber - valorPago);
            //lbSaldoPagar.Text = saldoPagar >= 0 ? saldoPagar.ToString("C2") : "R$ 0,00";
            //var troco = (valorPago - _valorReceber);
            //lbTroco.Text = troco >= 0 ? troco.ToString("C2") : "R$ 0,00";
           //CaixaItemView.Troco = troco > 0 ? troco : 0;

            //if(_valorReceber <= valorPago)
            //    btnPagar.PerformClick();
        }

        private void xButton1_Click(object sender, EventArgs e)
        {

            ResetControls();

            var btnPgto = (EspecieGridViewItem)sender;
            btnPgto.Selecionar();

            _especiePagamento = btnPgto.Especie;


            flayoutGrupo.Refresh();
            //var valorPago = Pagamentos.Sum(t => t.Valor);
            txtValor.ValueNumeric = 0;

            panel1.Focus();

            txtValor.Select();
        }

        private void ResetControls()
        {
            for (int i = 0; i < flayoutGrupo.Controls.Count; i++)
            {
                if (flayoutGrupo.Controls[i].GetType() != typeof(EspecieGridViewItem))
                    continue;

                ((EspecieGridViewItem)flayoutGrupo.Controls[i]).Selected = false;
                ((EspecieGridViewItem)flayoutGrupo.Controls[i]).Resetar();

            }

        }

        private void btnLancarPgamento_Click(object sender, EventArgs e)
        {
            if (_especiePagamento == null)
            {
                TouchMessageBox.Show("Informe a espécie de pagamento", "Finalizar", MessageBoxButtons.OK);
                return;
            }
            var valor = txtValor.ValueNumeric;

            var caixaItens = _caixaItemAppService.ObterPorCaixaId(caixaView.CaixaId);

            if (Pagamentos.Any(t => t.Especie == _especiePagamento.Especie))
                Pagamentos.FirstOrDefault(t => t.Especie == _especiePagamento.Especie).Valor += valor;

            var totalEspecie = caixaItens.Sum(t => t.CaixaPagamentos.Where(w => w.EspeciePagamentoId == _especiePagamento.EspeciePagamentoId).Sum(y => y.Valor));
            var divergencia = totalEspecie - valor;
            Pagamentos.Add(new CaixaFechamentoViewModel()
            {
                CaixaId = caixaView.CaixaId,
                EspeciePagamentoId = _especiePagamento.EspeciePagamentoId,
                Especie = _especiePagamento.Especie,
                Valor = valor,
                Divergencia = divergencia
            });


            LancarPagamento();
        }

        private void LancarPagamento()
        {
            flpResumoPagamento.Controls.Clear();
            foreach (var pdvPagamento in Pagamentos)
            {
                var resumoItem = new UcPdvItem();
                resumoItem.AdicionarCaixaFechamento(pdvPagamento);
                resumoItem.Dock = DockStyle.Top;
                resumoItem.Click += ResumoItem_Click;

                flpResumoPagamento.Controls.Add(resumoItem);
            }
            TotalizaPagamento();

            txtValor.ValueNumeric = 0;
            ResetControls();
            _especiePagamento = null;
        }

        private void ResumoItem_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem)sender;
            var caixaFechamento = (CaixaFechamentoViewModel) item.CaixaSource;
            if (Pagamentos.Any(t => t.Especie == caixaFechamento.Especie))
            {

                Pagamentos.Remove(caixaFechamento);
            }

            LancarPagamento();
            TotalizaPagamento();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void keyboardNum1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {
            if (_especiePagamento == null)
            {
                TouchMessageBox.Show("Informe a espécie de pagamento", "Receber", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                btnLancarPgamento.PerformClick();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                {
                    if (txtValor.SelectionLength > 0)
                        txtValor.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length - 1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(txtValor.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    txtValor.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / 100 : 0;
                }

            }
            txtValor.SelectionStart = txtValor.Text.Length;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            using (var caixAppService = Program.Container.GetInstance<ICaixaAppService>())
            {
                caixaView.CaixaFechamentos = Pagamentos;
                caixaView.UsuarioFinal = Program.Usuario.UsuarioId;

                caixAppService.Fechar(caixaView);
            }


            /*
             Imprime Fechamento do caixa
             */
            var parms = new ParameterReportDynamic();
            parms.Add("caixaId", caixaView.CaixaId);

            var report = new RelatorioFastReport();
            report.GerarRelatorio("Imp_FechamentoCaixa", parms);


            TouchMessageBox.Show("Caixa fechado com sucesso!", "Fechamento de caixa", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;

            Close();
        }
    }
}
