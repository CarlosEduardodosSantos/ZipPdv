namespace Zip.Pdv.Component.EspeciePagamento
{
    partial class ItemEspecie
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
            this.txtvTotal = new TextBoxDecimal();
            this.lbEspecie = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtvTotal);
            this.panel1.Controls.Add(this.lbEspecie);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 39);
            this.panel1.TabIndex = 0;
            // 
            // txtvTotal
            // 
            this.txtvTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtvTotal.BackColor = System.Drawing.Color.Snow;
            this.txtvTotal.BackColorEnter = System.Drawing.Color.Empty;
            this.txtvTotal.CasasDecimais = null;
            this.txtvTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvTotal.Location = new System.Drawing.Point(227, 4);
            this.txtvTotal.Name = "txtvTotal";
            this.txtvTotal.ReadOnly = true;
            this.txtvTotal.Size = new System.Drawing.Size(190, 29);
            this.txtvTotal.TabIndex = 8;
            this.txtvTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbEspecie
            // 
            this.lbEspecie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEspecie.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEspecie.Location = new System.Drawing.Point(8, 4);
            this.lbEspecie.Name = "lbEspecie";
            this.lbEspecie.Size = new System.Drawing.Size(213, 29);
            this.lbEspecie.TabIndex = 7;
            this.lbEspecie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ItemEspecie
            // 
            this.Controls.Add(this.panel1);
            this.Name = "ItemEspecie";
            this.Size = new System.Drawing.Size(423, 39);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private TextBoxDecimal txtvTotal;
        private System.Windows.Forms.Label lbEspecie;

    }
}
