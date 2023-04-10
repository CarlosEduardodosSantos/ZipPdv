using Eticket.Application;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Infra.Data.Repository;
using FastReport.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zip.Pdv.Component.CupomGrid;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Zip.Pdv
{
    public partial class FormMesas : Form
    {
        private readonly ICadMesasAppService _CadMesasAppService;
        private readonly IOpMesa2AppService _OpMesa2AppService;
        private readonly IOpMesa1AppService _OpMesa1AppService;
        public VendaViewModel venda;
        public CupomGridView cupomgridview;
        public CadMesasViewModel mesa;
        public FormMesas(VendaViewModel vd)
        {
            _OpMesa1AppService = Program.Container.GetInstance<IOpMesa1AppService>();
            _OpMesa2AppService = Program.Container.GetInstance<IOpMesa2AppService>();
            _CadMesasAppService = Program.Container.GetInstance<ICadMesasAppService>();
            InitializeComponent();
            venda = vd;
        }

        private void FormMesas_Load(object sender, EventArgs e)
        {
            var mesas = _CadMesasAppService.ObterMesas().ToList();

            int height = this.Size.Height;
            int width = this.Size.Width;

            int widthOffset = 10;
            int heightOffset = 10;

            int btnWidth = 75;  // Button Widht
            int btnHeight = 75;  // Button Height

            for (int i = 1; i < mesas.Count+1; i++)
            {
                var button = new Button();
                button.BackgroundImageLayout = ImageLayout.Center;
                button.TextAlign = ContentAlignment.BottomCenter;

                if ((widthOffset + btnWidth) >= width)
                {
                    widthOffset = 10;
                    heightOffset = heightOffset + btnHeight;
 
         
                    button.Size = new Size(btnWidth, btnHeight);
                    button.Name = "" + i + "";
                    button.Text = "" + i + "";
                    //button.Click += button_Click; // Button Click Event
                    button.Location = new Point(widthOffset, heightOffset);

                    Controls.Add(button);

                    widthOffset = widthOffset + (btnWidth);
                }

                else
                {
                    
                    button.Size = new Size(btnWidth, btnHeight);
                    button.Name = "" + i + "";
                    button.Text = "" + i + "";
                    //button.Click += button_Click; // Button Click Event
                    button.Location = new Point(widthOffset, heightOffset);

                    Controls.Add(button);

                    widthOffset = widthOffset + (btnWidth);
                }

                if (mesas[i-1].Status == 1)
                {
                    button.BackgroundImage = Properties.Resources.mesa_disponivel;
                    button.Click += (sender2, EventArgs) => { disponivel_Click(sender2, EventArgs); };

                }
                else if (mesas[i - 1].Status == 2)
                {
                    button.BackgroundImage = Properties.Resources.mesa_pendente;
                    button.Click += (sender2, EventArgs) => { pendente_Click(sender2, EventArgs); };
                }
                else
                {
                    button.BackgroundImage = Properties.Resources.mesa_ocupada;
                    button.Click += (sender2, EventArgs) => { ocupado_Click(sender2, EventArgs); };
                }

            }


        }
        private void disponivel_Click(object sender, EventArgs e)
        {
            var botao =  (Button)sender;
            var id = Int32.Parse(botao.Text);
            var myForm = new FormMesaDetalhes(venda, Int32.Parse(botao.Text));
            myForm.Show();
            myForm.FormClosing += (obj, args) => { this.Close(); };


        }

        private void pendente_Click(object sender, EventArgs e)
        {
            var botao = (Button)sender;
            var id = Int32.Parse(botao.Text);
            DialogResult dialog = MessageBox.Show("A Mesa "+ botao.Text + " Está Fechada! Deseja Abrir Novamente?", "Atenção!",MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                var myForm = new FormMesaDetalhes(venda, Int32.Parse(botao.Text));
                mesa = _CadMesasAppService.GetById(id);
                mesa.Status = 3;
                _CadMesasAppService.AlterarStatusMesa(mesa);
                myForm.Show();
                myForm.FormClosing += (obj, args) => { this.Close(); };
            }
 



        }

        private void ocupado_Click(object sender, EventArgs e)
        {

            var botao = (Button)sender;
            var id = Int32.Parse(botao.Text);
            var myForm = new FormMesaDetalhes(venda, Int32.Parse(botao.Text));
            myForm.Show();
            myForm.FormClosing += (obj, args) => { this.Close(); };


        }

        public void FormMesas_FormClosing(object sender, FormClosingEventArgs e)
        {
           //limpa o formPdv
        }
    }
}
