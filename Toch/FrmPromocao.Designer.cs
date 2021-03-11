namespace Toch
{
    partial class FrmPromocao
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
            this.lbProduto = new System.Windows.Forms.Label();
            this.btnValorVenda = new System.Windows.Forms.Button();
            this.btnValorPromocao = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnValorPromocao);
            this.panel1.Controls.Add(this.btnValorVenda);
            this.panel1.Controls.Add(this.lbProduto);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 133);
            this.panel1.TabIndex = 0;
            // 
            // lbProduto
            // 
            this.lbProduto.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProduto.Location = new System.Drawing.Point(0, 0);
            this.lbProduto.Name = "lbProduto";
            this.lbProduto.Size = new System.Drawing.Size(386, 29);
            this.lbProduto.TabIndex = 0;
            this.lbProduto.Text = "label1";
            this.lbProduto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnValorVenda
            // 
            this.btnValorVenda.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnValorVenda.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnValorVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValorVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValorVenda.ForeColor = System.Drawing.Color.Blue;
            this.btnValorVenda.Location = new System.Drawing.Point(0, 29);
            this.btnValorVenda.Name = "btnValorVenda";
            this.btnValorVenda.Size = new System.Drawing.Size(386, 48);
            this.btnValorVenda.TabIndex = 1;
            this.btnValorVenda.Text = "Valor de Venda....... R$ ";
            this.btnValorVenda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValorVenda.UseVisualStyleBackColor = true;
            this.btnValorVenda.Click += new System.EventHandler(this.btnValorVenda_Click);
            // 
            // btnValorPromocao
            // 
            this.btnValorPromocao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnValorPromocao.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnValorPromocao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValorPromocao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValorPromocao.ForeColor = System.Drawing.Color.Blue;
            this.btnValorPromocao.Location = new System.Drawing.Point(0, 77);
            this.btnValorPromocao.Name = "btnValorPromocao";
            this.btnValorPromocao.Size = new System.Drawing.Size(386, 48);
            this.btnValorPromocao.TabIndex = 2;
            this.btnValorPromocao.Text = "Valor de Promoção...... R$ ";
            this.btnValorPromocao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValorPromocao.UseVisualStyleBackColor = true;
            this.btnValorPromocao.Click += new System.EventHandler(this.btnValorPromocao_Click);
            // 
            // FrmPromocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 133);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPromocao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informe o valor do Produto";
            this.Load += new System.EventHandler(this.FrmPromocao_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbProduto;
        private System.Windows.Forms.Button btnValorPromocao;
        private System.Windows.Forms.Button btnValorVenda;
    }
}