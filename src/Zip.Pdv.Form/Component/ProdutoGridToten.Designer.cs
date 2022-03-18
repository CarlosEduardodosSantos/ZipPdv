
namespace Zip.Pdv.Component
{
    partial class ProdutoGridToten
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbProduto = new System.Windows.Forms.Label();
            this.lbValorVenda = new System.Windows.Forms.Label();
            this.imageProd = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageProd)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.imageProd);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 230);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Controls.Add(this.lbProduto);
            this.panel2.Controls.Add(this.lbValorVenda);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 74);
            this.panel2.TabIndex = 0;
            // 
            // lbProduto
            // 
            this.lbProduto.BackColor = System.Drawing.Color.Transparent;
            this.lbProduto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProduto.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProduto.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbProduto.Location = new System.Drawing.Point(0, 23);
            this.lbProduto.Name = "lbProduto";
            this.lbProduto.Size = new System.Drawing.Size(262, 51);
            this.lbProduto.TabIndex = 0;
            this.lbProduto.Text = "label1grgregrDDDDD gregrgrerertgtre";
            this.lbProduto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbValorVenda
            // 
            this.lbValorVenda.BackColor = System.Drawing.Color.Transparent;
            this.lbValorVenda.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbValorVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorVenda.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbValorVenda.Location = new System.Drawing.Point(0, 0);
            this.lbValorVenda.Name = "lbValorVenda";
            this.lbValorVenda.Size = new System.Drawing.Size(262, 23);
            this.lbValorVenda.TabIndex = 7;
            this.lbValorVenda.Text = "R$ 10.00";
            this.lbValorVenda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbValorVenda.Visible = false;
            // 
            // imageProd
            // 
            this.imageProd.BackColor = System.Drawing.Color.Transparent;
            this.imageProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageProd.ImageLocation = "";
            this.imageProd.Location = new System.Drawing.Point(0, 0);
            this.imageProd.Name = "imageProd";
            this.imageProd.Size = new System.Drawing.Size(262, 156);
            this.imageProd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageProd.TabIndex = 5;
            this.imageProd.TabStop = false;
            // 
            // ProdutoGridToten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ProdutoGridToten";
            this.Size = new System.Drawing.Size(262, 230);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageProd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox imageProd;
        private System.Windows.Forms.Label lbProduto;
        private System.Windows.Forms.Label lbValorVenda;
    }
}
