using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv.Component
{
    public partial class ImageView : UserControl
    {
        public event EventHandler<EventArgs> TaskItem;
        void taskItem(object sender, EventArgs e)
        {
            var completedEvent = TaskItem;
            if (completedEvent != null)
            {
                var item = (ImageView)this;
                completedEvent(item, e);
            }
        }

        public Image imagem { get { return this.pictureBox1.Image; } set { this.pictureBox1.Image = value; } }
        public ImageView()
        {

            InitializeComponent();
            btnExcluir.Click += taskItem;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }
    }
}
