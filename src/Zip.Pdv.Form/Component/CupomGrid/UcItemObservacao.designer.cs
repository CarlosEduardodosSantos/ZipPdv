namespace Zip.Pdv.Component.EspeciePagamento
{
    partial class UcItemObservacao
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
            this.btnDeletar = new System.Windows.Forms.Button();
            this.lbDisplay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnDeletar
            // 
            this.btnDeletar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeletar.FlatAppearance.BorderSize = 0;
            this.btnDeletar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletar.Image = global::Zip.Pdv.Properties.Resources.Trash_16;
            this.btnDeletar.Location = new System.Drawing.Point(243, 0);
            this.btnDeletar.Name = "btnDeletar";
            this.btnDeletar.Size = new System.Drawing.Size(23, 34);
            this.btnDeletar.TabIndex = 3;
            this.btnDeletar.UseVisualStyleBackColor = true;
            // 
            // lbDisplay
            // 
            this.lbDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisplay.Location = new System.Drawing.Point(0, 0);
            this.lbDisplay.Multiline = true;
            this.lbDisplay.Name = "lbDisplay";
            this.lbDisplay.Size = new System.Drawing.Size(243, 34);
            this.lbDisplay.TabIndex = 4;
            this.lbDisplay.Text = "Observação";
            this.lbDisplay.Enter += new System.EventHandler(this.lbDisplay_Enter);
            this.lbDisplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbDisplay_KeyDown);
            // 
            // UcItemObservacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lbDisplay);
            this.Controls.Add(this.btnDeletar);
            this.Name = "UcItemObservacao";
            this.Size = new System.Drawing.Size(266, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDeletar;
        private System.Windows.Forms.TextBox lbDisplay;
    }
}
