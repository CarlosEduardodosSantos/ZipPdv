namespace Zip.Pdv
{
    partial class FormComplementos
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbProdutoNome = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnPrevProd = new System.Windows.Forms.Button();
            this.btnNextProd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.lbProdutoNome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 110);
            this.panel1.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.AutoScrollMinSize = new System.Drawing.Size(0, 35);
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(798, 85);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // lbProdutoNome
            // 
            this.lbProdutoNome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbProdutoNome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdutoNome.Location = new System.Drawing.Point(0, 0);
            this.lbProdutoNome.Name = "lbProdutoNome";
            this.lbProdutoNome.Size = new System.Drawing.Size(798, 23);
            this.lbProdutoNome.TabIndex = 0;
            this.lbProdutoNome.Text = "PRODUTO";
            this.lbProdutoNome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(68, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(673, 426);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // panel23
            // 
            this.panel23.BackgroundImage = global::Zip.Pdv.Properties.Resources.bg_;
            this.panel23.Controls.Add(this.label2);
            this.panel23.Controls.Add(this.btnVoltar);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(800, 42);
            this.panel23.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(72, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(658, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "COMPLEMENTOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.Transparent;
            this.btnVoltar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVoltar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Image = global::Zip.Pdv.Properties.Resources.back_32;
            this.btnVoltar.Location = new System.Drawing.Point(0, 0);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(50, 42);
            this.btnVoltar.TabIndex = 4;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnPrevProd
            // 
            this.btnPrevProd.BackColor = System.Drawing.Color.LightGreen;
            this.btnPrevProd.BackgroundImage = global::Zip.Pdv.Properties.Resources.previous_resultset_icone_6623_32;
            this.btnPrevProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrevProd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevProd.Enabled = false;
            this.btnPrevProd.FlatAppearance.BorderSize = 0;
            this.btnPrevProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevProd.Location = new System.Drawing.Point(0, 0);
            this.btnPrevProd.Name = "btnPrevProd";
            this.btnPrevProd.Size = new System.Drawing.Size(68, 426);
            this.btnPrevProd.TabIndex = 7;
            this.btnPrevProd.UseVisualStyleBackColor = false;
            this.btnPrevProd.Click += new System.EventHandler(this.btnPrevProd_Click);
            // 
            // btnNextProd
            // 
            this.btnNextProd.BackColor = System.Drawing.Color.LightGreen;
            this.btnNextProd.BackgroundImage = global::Zip.Pdv.Properties.Resources.next_resultset_icone_3882_32;
            this.btnNextProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNextProd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextProd.Enabled = false;
            this.btnNextProd.FlatAppearance.BorderSize = 0;
            this.btnNextProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextProd.Location = new System.Drawing.Point(741, 0);
            this.btnNextProd.Name = "btnNextProd";
            this.btnNextProd.Size = new System.Drawing.Size(57, 426);
            this.btnNextProd.TabIndex = 8;
            this.btnNextProd.UseVisualStyleBackColor = false;
            this.btnNextProd.Click += new System.EventHandler(this.btnNextProd_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.btnNextProd);
            this.panel2.Controls.Add(this.btnPrevProd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 428);
            this.panel2.TabIndex = 9;
            // 
            // FormComplementos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel23);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormComplementos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormComplementos";
            this.Load += new System.EventHandler(this.FormComplementos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbProdutoNome;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnPrevProd;
        private System.Windows.Forms.Button btnNextProd;
        private System.Windows.Forms.Panel panel2;
    }
}