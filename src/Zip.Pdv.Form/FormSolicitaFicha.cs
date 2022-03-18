using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormSolicitaFicha : Form
    {
        private string _ficha;
        private string _title { set => label2.Text = value; }
        private bool _obrigatorio;
        public List<int> Fichas;
        public FormSolicitaFicha()
        {
            InitializeComponent();
            Fichas = new List<int>();
        }

        public static int[] Instace(string title, bool obrigatorio = false)
        {

            using (var form = new FormSolicitaFicha())
            {
                form._title = title;
                form._obrigatorio = obrigatorio;
                var result = form.ShowDialog();

                return result == DialogResult.OK ? form.Fichas.ToArray() : null;
            }
        }

        private void CarregaFichas()
        {
            flayoutGrupo.Controls.Clear();
            foreach (var ficha in Fichas)
            {
                var item = new UcPdvItem();
                item.AdicionarFicha(ficha);
                item.Click += Item_Click;
                flayoutGrupo.Controls.Add(item);

            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem)sender;
            var source = (int)item.CaixaSource;
            Fichas.Remove(source);
            CarregaFichas();
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!_obrigatorio)
                DialogResult = DialogResult.Cancel;
        }
        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
        }

        private void FormDesconto_Load(object sender, EventArgs e)
        {
            txtFicha.Select();
        }

        private void txtCpf_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Enter) return;


            ValidaFicha();

        }
        private void ValidaFicha()
        {
            Guid guidOutput;
            bool isGuid = Guid.TryParse(txtFicha.Text, out guidOutput);
            if (isGuid)
                LancaFichaGuid(guidOutput);
            else
            {
                var fichaId = 0;
                if (!int.TryParse(txtFicha.Text, out fichaId))
                {
                    Funcoes.MensagemError("Ficha inválida.\nVerifica e tente novamente.");
                    txtFicha.Clear();
                    txtFicha.Focus();

                    return;
                }
                LancaFicha(fichaId);
            }

        }
        private void LancaFicha(int fichaId)
        {
            
            using (var vendaFichaApp = Program.Container.GetInstance<IVendaFichaAppService>())
            {
                var fichaExists = vendaFichaApp.FicheExiste(fichaId);
                if (!fichaExists)
                {
                    Funcoes.MensagemError("Ficha não encontrada.\nVerifica e tente novamente.");
                    txtFicha.Clear();
                    txtFicha.Focus();

                    return;
                }

            }

            _ficha = fichaId.ToString(); ;
            Fichas.Add(int.Parse(_ficha));
            CarregaFichas();
            txtFicha.Clear();
            txtFicha.Focus();

        }

        private void LancaFichaGuid(Guid fichaGuid)
        {

            using (var vendaFichaApp = Program.Container.GetInstance<IVendaFichaAppService>())
            {
                var ficha = vendaFichaApp.ObterFichaByGuid(fichaGuid.ToString());
                if (ficha == null)
                {
                    Funcoes.MensagemError("Ficha não encontrada.\nVerifica e tente novamente.");
                    txtFicha.Clear();
                    txtFicha.Focus();

                    return;
                }
                _ficha = ficha.Ficha.ToString();
                Fichas.Add(int.Parse(_ficha));
                CarregaFichas();
                txtFicha.Clear();
                txtFicha.Focus();
            }

        }


        private void FormSolicitaFicha_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_obrigatorio && string.IsNullOrEmpty(_ficha))
            {
                var result = TouchMessageBox.Show("É obrigatório informar o numero do pager!",
                    "Venda Finaliza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFicha.Focus();
                e.Cancel = true;
            }

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (Fichas.Count == 0 && !string.IsNullOrEmpty(txtFicha.Text))
            {
                ValidaFicha();
            }

            DialogResult = Fichas.Count == 0 ? DialogResult.Cancel : DialogResult.OK;

            Close();
        }
    }
}
