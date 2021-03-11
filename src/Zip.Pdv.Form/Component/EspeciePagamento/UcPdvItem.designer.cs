namespace Zip.Pdv.Component.EspeciePagamento
{
    partial class UcPdvItem
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
            this.lbDisplay = new System.Windows.Forms.Label();
            this.lbValue = new System.Windows.Forms.Label();
            this.btnDeletar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbDisplay
            // 
            this.lbDisplay.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbDisplay.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplay.Location = new System.Drawing.Point(0, 0);
            this.lbDisplay.Name = "lbDisplay";
            this.lbDisplay.Size = new System.Drawing.Size(136, 34);
            this.lbDisplay.TabIndex = 1;
            this.lbDisplay.Text = "Cartão Débito";
            this.lbDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbValue
            // 
            this.lbValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbValue.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValue.Location = new System.Drawing.Point(142, 0);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(98, 34);
            this.lbValue.TabIndex = 2;
            this.lbValue.Text = "R$ 40,00";
            this.lbValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDeletar
            // 
            this.btnDeletar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDeletar.FlatAppearance.BorderSize = 0;
            this.btnDeletar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletar.Image = global::Zip.Pdv.Properties.Resources.Trash_16;
            this.btnDeletar.Location = new System.Drawing.Point(241, 3);
            this.btnDeletar.Name = "btnDeletar";
            this.btnDeletar.Size = new System.Drawing.Size(23, 28);
            this.btnDeletar.TabIndex = 3;
            this.btnDeletar.UseVisualStyleBackColor = true;
            // 
            // UcPdvItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnDeletar);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.lbDisplay);
            this.Name = "UcPdvItem";
            this.Size = new System.Drawing.Size(266, 34);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbDisplay;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.Button btnDeletar;
    }
}
