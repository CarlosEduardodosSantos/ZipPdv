namespace Zip.Pdv.Component.EspeciePagamento
{
    partial class EspecieGridViewItem
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
            this.lbEspecie = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbEspecie
            // 
            this.lbEspecie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEspecie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEspecie.ForeColor = System.Drawing.Color.White;
            this.lbEspecie.Location = new System.Drawing.Point(0, 0);
            this.lbEspecie.Name = "lbEspecie";
            this.lbEspecie.Size = new System.Drawing.Size(127, 50);
            this.lbEspecie.TabIndex = 0;
            this.lbEspecie.Text = "Dinheiro";
            this.lbEspecie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EspecieGridViewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(166)))), ((int)(((byte)(222)))));
            this.Controls.Add(this.lbEspecie);
            this.Name = "EspecieGridViewItem";
            this.Size = new System.Drawing.Size(127, 50);
            this.Load += new System.EventHandler(this.EspecieGridViewItem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbEspecie;
    }
}
