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
    public partial class FrmMeioMeio : Form
    {
        public string DescricaoItens { get; set; }
        public FrmMeioMeio()
        {
            InitializeComponent();
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Panel2Collapsed = true;
            
        }
        private Color CorStatus(string Situacao)
        {

            if (Situacao == "A")
                return Color.Red;
            if (Situacao == "L")
                return System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(56))))); ;
            if (Situacao == "F")
                return Color.Goldenrod;
            if (Situacao == "")
                return System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(56))))); ;

            return System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(56))))); ;
        }

        private void FrmMeioMeio_Load(object sender, EventArgs e)
        {
         

            clsProdutos produtos = new clsProdutos();
            /*
            panelViewDetail1.ValueDecimal = "";
            panelViewDetail1.DisplayVaue = "Descricao";
            panelViewDetail1.ValueInt = "IdMesa";
            panelViewDetail1.ValueString2 = "Situacao";
            panelViewDetail1.DataSource(produtos.ListaMesas());
            panelViewDetail1.OnClickDetailIten += new OnClickDetailIten(ClickIten);
            panelViewDetail1.AutoScroll = true;
            panelViewDetail1.VerticalScroll.Enabled = false;
            foreach (DetailIten iten in panelViewDetail1.Controls)
            {
                iten.BackColor = CorStatus(iten.ValueString2);
            }
            */
            
            var ListaProdutos = produtos.ListaGrupo();
            panelViewDetail1.DataSource(ListaProdutos);
            panelViewDetail1.OnClickDetailIten += new OnClickDetailIten(ClickIten);
            
            /*
            //Cria a instancia com o formulário a ser aberto
            FrmPrincipal frm = new FrmPrincipal();
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //Seta as configuração do novo Form
            //No meu caso, eu utilizo o Form maximizado, entao:
            frm.WindowState = FormWindowState.Maximized;
            frm.Dock = DockStyle.Fill;
            frm.Width = panelViewDetail1.Width;
            frm.Height = panelViewDetail1.Height;

            frm.TopLevel = false;
            frm.Show();
            //Adiciona ao splitContainer1, no Painel2, o novo Form instaciado
            panelViewDetail1.Controls.Add(frm);
            */
            


        }
        protected virtual void ClickIten(object sender, EventArgs e)
        {
            DetailIten btn = (DetailIten)sender;
            splitContainer1.Panel2Collapsed = false;
            //blNomeProduto.Text = btn.Text;
            //MessageBox.Show(btn.Text);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
