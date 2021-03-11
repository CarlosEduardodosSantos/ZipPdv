using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zip.EticketSub.Repository;

namespace Zip.EticketSub
{
    public partial class FormEnviaSat : Form
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly int _pedidoId;
        private string _statusCaption;
        public FormEnviaSat(int pedidoId)
        {
            _pedidoId = pedidoId;
            InitializeComponent();
            _pedidoRepository = new PedidoRepository();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var concentradorId = Program.ConcentradorId;
            var statuss = 0;
            var funcao =1;
            var fabricante = Program.SatMarca;
            var sessao = 0;
            var codigoAtivacao = Program.CodigoAtivacao;
            var chave = "";
            var xml = "";
            var pcNome = "";
            var empresaId = Program.Loja;
            var pdv = Program.Pdv;
            var numeroSerie = Program.SerieSat;

            var retorno = _pedidoRepository.ImprimeSat(concentradorId, statuss, funcao, fabricante, sessao,
                codigoAtivacao, chave, xml, pcNome, _pedidoId, empresaId, pdv, numeroSerie);

            var ok = true;
            while (ok)
            {
                var status = _pedidoRepository.ObterSatusSat(retorno);

                switch (status.Status)
                {
                    case 0:
                        _statusCaption = "SAT - Aguardando execução...";
                        break;
                    case 1:
                        _statusCaption = "SAT - Em execução...";
                        break;
                    case 2:
                        if (status.Sucesso)
                            _statusCaption = "SAT - Execução finalizada. Imprimindo...";
                        else
                            _statusCaption = "SAT - Execução finalizada.";
                        ok = false;
                        break;
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbStatus.Text = _statusCaption;
        }
    }
}
