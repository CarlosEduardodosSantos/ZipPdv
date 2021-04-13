namespace Zip.Pdv.Component
{
    partial class GrupoGridViewItem
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnIcom = new System.Windows.Forms.PictureBox();
            this.lbGrupo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnIcom)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.03846F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.96154F));
            this.tableLayoutPanel1.Controls.Add(this.btnIcom, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbGrupo, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(130, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnIcom
            // 
            this.btnIcom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnIcom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnIcom.ImageLocation = "";
            this.btnIcom.Location = new System.Drawing.Point(99, 3);
            this.btnIcom.Name = "btnIcom";
            this.btnIcom.Size = new System.Drawing.Size(28, 49);
            this.btnIcom.TabIndex = 2;
            this.btnIcom.TabStop = false;
            // 
            // lbGrupo
            // 
            this.lbGrupo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGrupo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrupo.Location = new System.Drawing.Point(3, 0);
            this.lbGrupo.Name = "lbGrupo";
            this.lbGrupo.Size = new System.Drawing.Size(90, 55);
            this.lbGrupo.TabIndex = 1;
            this.lbGrupo.Text = "label1";
            this.lbGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GrupoGridViewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GrupoGridViewItem";
            this.Size = new System.Drawing.Size(130, 55);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnIcom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbGrupo;
        private System.Windows.Forms.PictureBox btnIcom;
    }
}
