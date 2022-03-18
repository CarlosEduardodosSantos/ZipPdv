using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Zip.Pdv.Page
{
    public partial class PagePrincipalTotem : UserControl
    {
        public UserControl Page;
        public event EventHandler<EventArgs> SelectItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (Button)sender;
                completedEvent(item, e);
            }
        }

        private static PagePrincipalTotem _instance;
        public static PagePrincipalTotem Instance => _instance ?? (_instance = new PagePrincipalTotem());
        public PagePrincipalTotem()
        {
            InitializeComponent();


            
        }

        private void PagePrincipalTotem_Load(object sender, EventArgs e)
        {
            btnPagamento.Visible = Program.TotemHabPagamento;
            btnPedido.Visible = Program.TotemHabPedido;

            if (!string.IsNullOrEmpty(Program.MensagemTotem))
                label1.Text = Program.MensagemTotem;

            var fileBackGroudImage = $"{AppDomain.CurrentDomain.BaseDirectory}Fundo.Jpg";
            if (File.Exists(fileBackGroudImage))
                panel1.BackgroundImage = Image.FromFile(fileBackGroudImage);

            var logo = $"{AppDomain.CurrentDomain.BaseDirectory}Logo.Png";
            if (File.Exists(logo))
                pictureBox1.Image = Image.FromFile(logo);
        }

      

    }
}
