using Eticket.Application;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Zip.Pdv.NFce
{
    public partial class FrmNfceNaoEnviada : Form
    {
        private List<NFceViewModel> _nfces;
        public FrmNfceNaoEnviada()
        {
            InitializeComponent();
        }

        private void FrmNfceNaoEnviada_Load(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            try
            {
                using (var nfeServico = Program.Container.GetInstance<INfceServicoAppService>())
                {
                    DateTime primeiroDiaDoMes = new DateTime(dtpInicio.Value.Year, dtpInicio.Value.Month, 1);

                    _nfces = nfeServico.NfceNãoEnviadas(primeiroDiaDoMes.Date).ToList();

                    dgvVendas.AutoGenerateColumns = false;
                    dgvVendas.DataSource = _nfces;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnPesquisaVenda_Click(object sender, EventArgs e)
        {
            foreach (var item in _nfces)
            {
                using (var nfeServico = Program.Container.GetInstance<INfceServicoAppService>())
                {
                    try
                    {
                        nfeServico.GeraNFce(item.NfceId);

                        var txtNfce = nfeServico.GeraNFce(item.NfceId);

                        if (string.IsNullOrEmpty(txtNfce)) return;

                        var diretorio = nfeServico.ObterDiretorioNfce(Program.Loja);

                        File.WriteAllText($"{diretorio}NotaFiscal_{item.NumeroNfce}.txt", txtNfce);
                        Thread.Sleep(8000);
                    }
                    catch (Exception)
                    {

                        continue;
                    }

                }
            }
            CarregaGrid();
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            CarregaGrid();
        }
    }
}
