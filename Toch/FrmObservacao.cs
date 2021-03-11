using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toch
{
    public partial class FrmObservacao : Form
    {
        public string DescricaoProd { get; set; }
        public string Observacao { get; set; }

        private clsMesa operacao = new clsMesa();
        public FrmObservacao()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;
        }

        private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardClassLibrary.KeyboardEventArgs e)
        {
            richTextBox1.Focus();
            if (e.KeyboardKeyPressed == "{ENTER}")
            {
                Observacao = richTextBox1.Text;
                DescricaoProd = lbDesProd.Text + richTextBox1.Text;
                if (!ListaSugestao.Exists(P => P.Sugestao == richTextBox1.Text))
                {
                    operacao.GravaSugestao(richTextBox1.Text);
                }
                this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                    SendKeys.Send(e.KeyboardKeyPressed.ToUpper());
            }
        }
        List<SugestaoObs> ListaSugestao;

        private void AtualizaSugestao()
        {
            foreach (SugestaoObs itens in ListaSugestao)
            {
                TextBoxProd btn = new TextBoxProd();
                btn.Height = 53;
                btn.Width = 122;
                btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                btn.ForeColor = Color.Black;

                btn.Text = itens.Sugestao;
                if (pnlSugestao.Controls.Count * btn.Width >= pnlSugestao.Width)
                {
                    int i = (pnlSugestao.Controls.Count * btn.Width) / pnlSugestao.Width;
                    btn.Left = (i-1) * btn.Width;
                    btn.Top = btn.Height * i -1;
                }
                else
                {
                    btn.Left = pnlSugestao.Controls.Count * btn.Width;
                    btn.Top = 0;
                }
                btn.Parent = pnlSugestao;
                btn.Click += Btn_Click;
            }
            if (ListaSugestao.Count > 0)
                splitContainer1.Panel2Collapsed = false;
            else
                splitContainer1.Panel2Collapsed = true;
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            String Sugestao = ((TextBoxProd)sender).Text;
            richTextBox1.Text = Sugestao;
            operacao.AtualizaSugestao(Sugestao);

        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {


        }

        private void FrmObservacao_Load(object sender, EventArgs e)
        {
            ListaSugestao = new List<SugestaoObs>();
            ListaSugestao = operacao.ListaSugestaoObs("%%");
            AtualizaSugestao();
            lbDesProd.Text = DescricaoProd + " ";
            richTextBox1.Select();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ListaSugestao = new List<SugestaoObs>();
            ListaSugestao = operacao.ListaSugestaoObs(richTextBox1.Text);
            AtualizaSugestao();
        }
    }
}