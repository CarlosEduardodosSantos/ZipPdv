namespace Zip.Pdv.Component
{
    partial class ProdutoGridViewItem
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
            this.lbGrupo = new System.Windows.Forms.Label();
            this.lbValorVenda = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbGrupo
            // 
            this.lbGrupo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGrupo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrupo.ForeColor = System.Drawing.Color.White;
            this.lbGrupo.Location = new System.Drawing.Point(0, 0);
            this.lbGrupo.Name = "lbGrupo";
            this.lbGrupo.Size = new System.Drawing.Size(155, 55);
            this.lbGrupo.TabIndex = 2;
            this.lbGrupo.Text = "label1";
            this.lbGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbValorVenda
            // 
            this.lbValorVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorVenda.ForeColor = System.Drawing.SystemColors.Info;
            this.lbValorVenda.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbValorVenda.Location = new System.Drawing.Point(78, 38);
            this.lbValorVenda.Name = "lbValorVenda";
            this.lbValorVenda.Size = new System.Drawing.Size(74, 14);
            this.lbValorVenda.TabIndex = 3;
            this.lbValorVenda.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // ProdutoGridViewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(217)))));
            this.Controls.Add(this.lbValorVenda);
            this.Controls.Add(this.lbGrupo);
            this.Name = "ProdutoGridViewItem";
            this.Size = new System.Drawing.Size(155, 55);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbGrupo;
        private System.Windows.Forms.Label lbValorVenda;
    }
}
