using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zip.Pdv.Component
{
    public partial class GalleryItem : UserControl
    {
        public event EventHandler<EventArgs> TaskItem;
        void taskItem(object sender, EventArgs e)
        {
            var completedEvent = TaskItem;
            if (completedEvent != null)
            {
                var item = (GalleryItem)this;
                completedEvent(item, e);
            }
        }
        public GalleryItem()
        {
            InitializeComponent();
            btnTask.Click += taskItem;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public int ItemIndex { get; set; }
        public Image imagem { get { return this.pictureBox1.Image; } set { this.pictureBox1.Image = value; } }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
