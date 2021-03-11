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
    public partial class TouchMessageBox : Form
    {

        static TouchMessageBox newMessageBox; 


        public TouchMessageBox()
        {
            InitializeComponent();
            
        }

        public static DialogResult Show(string text)
        {
            Button btn = new Button();
            btn.Size = new Size(29, 36);
            btn.Location = new Point(390, 108);
            btn.DialogResult = DialogResult.OK;

            newMessageBox.Controls.Add(btn);

            newMessageBox = new TouchMessageBox();
            newMessageBox.lbText.Text = text;
            if (newMessageBox.ShowDialog() == DialogResult.OK)
                return DialogResult.OK;
            else
                return DialogResult.Cancel;
        }

        public static DialogResult Show(string text, string caption)
        {
            Button btn = new Button();
            btn.Bounds = new Rectangle(390,108,90,40);
            btn.Text = "Confirmar";
            btn.DialogResult = DialogResult.OK;

            newMessageBox = new TouchMessageBox();
            newMessageBox.panel1.Controls.Add(btn);
            newMessageBox.Text = caption;
            newMessageBox.lbText.Text = text;
            newMessageBox.pictureBox2.Visible = false;
            if (newMessageBox.ShowDialog() == DialogResult.OK)
                return DialogResult.OK;
            else
                return DialogResult.Cancel;
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttos)
        {
            newMessageBox = new TouchMessageBox();
            if (buttos == MessageBoxButtons.OKCancel)
            {
                Button btnCancel = new Button();
                btnCancel.Bounds = new Rectangle(390, 108, 90, 40);
                btnCancel.Text = "Cancelar";
                btnCancel.DialogResult = DialogResult.Cancel;
                newMessageBox.panel1.Controls.Add(btnCancel);

                Button btnOk = new Button();
                btnOk.Bounds = new Rectangle(277, 108, 90, 40);
                btnOk.Text = "Confirmar";
                btnOk.DialogResult = DialogResult.OK;
                newMessageBox.panel1.Controls.Add(btnOk);
            }
            if (buttos == MessageBoxButtons.OK)
            {
                Button btnOk = new Button();
                btnOk.Bounds = new Rectangle(390, 108, 90, 40);
                btnOk.Text = "Confirmar";
                btnOk.DialogResult = DialogResult.OK;
                newMessageBox.panel1.Controls.Add(btnOk);
            }

            newMessageBox.Text = caption;
            newMessageBox.lbText.Text = text;
            newMessageBox.pictureBox2.Visible = false;
            if (newMessageBox.ShowDialog() == DialogResult.OK)
                return DialogResult.OK;
            else
                return DialogResult.Cancel;
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttos, MessageBoxIcon BoxIcon)
        {
            newMessageBox = new TouchMessageBox();
            if (buttos == MessageBoxButtons.OKCancel)
            {
                Button btnOk = new Button();
                btnOk.Bounds = new Rectangle(277, 108, 90, 40);
                btnOk.Text = "Confirmar";
                btnOk.DialogResult = DialogResult.OK;
                newMessageBox.panel1.Controls.Add(btnOk);

                Button btnCancel = new Button();
                btnCancel.Bounds = new Rectangle(390, 108, 90, 40);
                btnCancel.Text = "Cancelar";
                btnCancel.DialogResult = DialogResult.Cancel;
                newMessageBox.panel1.Controls.Add(btnCancel);
               
            }
            if (buttos == MessageBoxButtons.OK)
            {
                Button btnOk = new Button();
                btnOk.Bounds = new Rectangle(390, 108, 90, 40);
                btnOk.Text = "Confirmar";
                btnOk.DialogResult = DialogResult.OK;
                newMessageBox.panel1.Controls.Add(btnOk);
            }

            switch (BoxIcon)
	        {
                case MessageBoxIcon.Exclamation: newMessageBox.pictureBox2.Image = Properties.Resources._1340644414_document_properties;
                 break;
                case MessageBoxIcon.Question: newMessageBox.pictureBox2.Image = Properties.Resources._1340644254_question_blue;
                 break;
                case MessageBoxIcon.Error: newMessageBox.pictureBox2.Image = Properties.Resources.Falha;
                 break;
	        }
            newMessageBox.Text = caption;
            newMessageBox.lbText.Text = text;
            if (newMessageBox.ShowDialog() == DialogResult.OK)
                return DialogResult.OK;
            else
                return DialogResult.Cancel;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

    }
}
