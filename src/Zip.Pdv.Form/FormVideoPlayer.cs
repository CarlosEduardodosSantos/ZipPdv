using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv
{
    public partial class FormVideoPlayer : Form
    {
        public FormVideoPlayer()
        {
            InitializeComponent();
        }

        private void FormVideoPlayer_Load(object sender, EventArgs e)
        {
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";

            html += "<iframe id='video' src= 'https://www.youtube.com/watch?v=CKnGXZxK7zs' width='820' height='500' frameborder='0' allowfullscreen></iframe>";

            html += "</body></html>";
            this.webBrowser1.DocumentText = html;
        }
    }
}
