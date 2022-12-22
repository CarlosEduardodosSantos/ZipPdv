using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Fast;

namespace Zip.Pdv.Page
{
    public partial class PageCaixaMovimentacao : PageBase
    {
        private List<CaixaItemViewModel> _itemViewModels;
        public PageCaixaMovimentacao()
        {
            InitializeComponent();
            this.btnVoltar.Click += closeForm;
        }

        private void PageCaixaMovimentacao_Load(object sender, EventArgs e)
        {
            CarregaCaixa(Program.CaixaView);
        }

        private void CarregaCaixa(CaixaViewModel caixaView)
        {
            txtnCaixa.Text = caixaView.CaixaId.ToString();
            txtPDV.Text = caixaView.Pdv.ToString();
            txtDthCaixa.Text = caixaView.DataInicio.ToString();
            txtOperador.Text = Program.Usuario.Nome;

            txtdthFechamento.Text = caixaView.DataFinal.ToString();
            txtoperadorPechamento.Text = caixaView.UsuarioFinal;

            using (var caixaItemAppService = Program.Container.GetInstance<ICaixaItemAppService>())
            {
                _itemViewModels = caixaItemAppService.ObterPorCaixaId(caixaView.CaixaId).ToList();

                txtValorInicial.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "INI").Sum(t => t.Valor + t.Troco);
                txtvVendas.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "VDA" || t.TipoLancamento == "TEL").Sum(t => t.Valor + t.Troco);

                txtSuprimentos.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "SU").Sum(t => t.Valor);
                txtSangrias.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "SA").Sum(t => t.Valor);


                txtvTotalCaixa.ValueNumeric = _itemViewModels.Sum(t => t.Valor + t.Troco);

                txtVendaVista.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "VDA" || t.TipoLancamento == "TEL").Sum(t => t.Valor + t.Troco);
                txtvendaPrazo.ValueNumeric = 0;
                txtTotalVendasCancelada.ValueNumeric = 0;

                txtTotalRecebimentos.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "REC").Sum(t => t.Valor + t.Troco);
                txtvPagamentos.ValueNumeric = 0;


                var caixaItens = _itemViewModels
                    .SelectMany(m => m.CaixaPagamentos?.Select(e =>
                    new
                    {
                        m.TipoLancamento,
                        // m.DataHora,
                        m.VendaId,
                        m.Historico,
                        Especie = e != null ? e.Especie : "DINHEIRO",
                        Valor = e != null ? e.Valor : m.Valor,
                        m.CaixaItemId
                    })).OrderBy(o => o.VendaId).ToList();


                dgvHistCaixa.AutoGenerateColumns = false;
                dgvHistCaixa.DataSource = caixaItens;

                var especie = caixaItens.GroupBy(l => l.Especie).Select(sel => new
                {
                    Especie = sel.Key,
                    Valor = sel.Sum(t => t.Valor)
                }).ToList();


                dgvEspecies.AutoGenerateColumns = false;
                dgvEspecies.DataSource = especie;


                chart1.Series.Clear();

                for (int i = 0; i < especie.Count; i++)
                {

                    //double dataPoint = double.Parse(cxHist.Where(m => m.IdEspecie == especies[i].IdEspecie && m.vEspecie > 0).Sum(m => m.vEspecie).ToString());
                    double dataPoint = double.Parse(especie[i].Valor.ToString());
                    string series = especie[i].Especie + " " + dataPoint.ToString("C2");
                    if (dataPoint > 0)
                    {
                        chart1.Series.Add(new Series()
                        {
                            Name = series,
                            ChartType = SeriesChartType.Column

                        });
                        chart1.Series[series].Points.Clear();

                        DataPoint objDataPoint = new DataPoint() { Label = dataPoint.ToString(), AxisLabel = "series", YValues = new double[] { dataPoint } };
                        chart1.Series[series].Points.Add(dataPoint);
                    }
                }

            }

        }

        private void dgvHistCaixa_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((decimal)dgvHistCaixa.Rows[e.RowIndex].Cells[4].Value < 0)
            {
                dgvHistCaixa.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                dgvHistCaixa.Rows[e.RowIndex].DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            var caixaView = FormCaixaPesquisa.RetornaCaixaPesquisa();
            if (caixaView == null)
                return;


            CarregaCaixa(caixaView);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdthFechamento.Text))
            {

            }
            int caixaId;
            if (int.TryParse(txtnCaixa.Text, out caixaId))
            {

                var parms = new ParameterReportDynamic();
                parms.Add("caixaId", caixaId);

                var report = new RelatorioFastReport();
                report.GerarRelatorio("Imp_FechamentoCaixa", parms);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new FormCaixaLancamento())
            {
                form.ShowDialog();
                CarregaCaixa(Program.CaixaView);
            }
        }

        private void excluirRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var usuarioAppService = Program.Container.GetInstance<IUsuarioAppService>())
            {
                var dirCaixaMov = usuarioAppService.VerificaPrivilegio("CaixaGerencial", Program.Usuario.UsuarioId);
                if (!dirCaixaMov)
                {
                    TouchMessageBox.Show("Acesso não permitido.?", "Caixa");
                    return;
                }

                var result = TouchMessageBox.Show("Confirma a exclusão do lançamento?", "Caixa", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question);

                if (result != DialogResult.OK)
                    return;

                var row = dgvHistCaixa.SelectedRows[0];

                var caixaItemId = row.Cells[5].Value.ToString();
                var tipoDocumento = row.Cells[1].Value.ToString();

                string[] demoArray = { "SU", "SA"};

                if (demoArray.Contains(tipoDocumento) == false)
                {
                    TouchMessageBox.Show("Esse tipo de registro não pode ser excluido.", "Caixa");
                    return;
                }

                //SU
                using (var caixaItemAppService = Program.Container.GetInstance<ICaixaItemAppService>())
                {
                    caixaItemAppService.Remover(caixaItemId);
                    CarregaCaixa(Program.CaixaView);
                }

            }
        }
    }
}
