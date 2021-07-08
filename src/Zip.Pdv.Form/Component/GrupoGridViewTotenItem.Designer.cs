namespace Zip.Pdv.Component
{
    partial class GrupoGridViewTotenItem
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
            this.lbGrupo = new System.Windows.Forms.Label();
            this.btnIcom = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnIcom)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbGrupo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnIcom, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(130, 99);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbGrupo
            // 
            this.lbGrupo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGrupo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrupo.Location = new System.Drawing.Point(3, 71);
            this.lbGrupo.Name = "lbGrupo";
            this.lbGrupo.Size = new System.Drawing.Size(124, 28);
            this.lbGrupo.TabIndex = 0;
            this.lbGrupo.Text = "label1";
            this.lbGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnIcom
            // 
            this.btnIcom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnIcom.Location = new System.Drawing.Point(3, 3);
            this.btnIcom.Name = "btnIcom";
            this.btnIcom.Size = new System.Drawing.Size(124, 65);
            this.btnIcom.TabIndex = 1;
            this.btnIcom.TabStop = false;
            // 
            // GrupoGridViewTotenItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GrupoGridViewTotenItem";
            this.Size = new System.Drawing.Size(130, 99);
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
