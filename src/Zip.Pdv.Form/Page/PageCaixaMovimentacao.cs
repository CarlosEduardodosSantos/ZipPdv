﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;

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

            using (var caixaItemAppService = Program.Container.GetInstance<ICaixaItemAppService>())
            {
                _itemViewModels = caixaItemAppService.ObterPorCaixaId(caixaView.CaixaId).ToList();

                txtValorInicial.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "INI").Sum(t => t.Valor+t.Troco);
                txtvVendas.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "VDA" || t.TipoLancamento == "TEL").Sum(t => t.Valor + t.Troco);

                txtSuprimentos.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "SU").Sum(t => t.Valor);
                txtSangrias.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "SA").Sum(t => t.Valor); ;

                
                txtvTotalCaixa.ValueNumeric = _itemViewModels.Sum(t => t.Valor + t.Troco);

                txtVendaVista.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "VDA" || t.TipoLancamento == "TEL").Sum(t => t.Valor + t.Troco);
                txtvendaPrazo.ValueNumeric = 0;
                txtTotalVendasCancelada.ValueNumeric = 0;
                
                txtTotalRecebimentos.ValueNumeric = _itemViewModels.Where(t => t.TipoLancamento == "REC").Sum(t => t.Valor + t.Troco);
                txtvPagamentos.ValueNumeric = 0;


                var caixaItens = _itemViewModels
                    .SelectMany(m => m.CaixaPagamentos.Select(e =>
                    new
                    {
                        m.TipoLancamento,
                        m.DataHora,
                        m.VendaId,
                        m.Historico,
                        e.Especie,
                        e.Valor
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

    }
}