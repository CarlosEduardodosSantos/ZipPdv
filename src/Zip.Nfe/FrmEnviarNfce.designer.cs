namespace Zip.Nfe
{
    partial class FrmEnviarNfce
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
            this.btnRepetir = new System.Windows.Forms.Button();
            this.txtxMotivo = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRepetir);
            this.panel1.Controls.Add(this.txtxMotivo);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 152);
            this.panel1.TabIndex = 9;
            // 
            // btnRepetir
            // 
            this.btnRepetir.Location = new System.Drawing.Point(11, 44);
            this.btnRepetir.Name = "btnRepetir";
            this.btnRepetir.Size = new System.Drawing.Size(451, 23);
            this.btnRepetir.TabIndex = 10;
            this.btnRepetir.Tag = "0";
            this.btnRepetir.Text = "Tentar enviar novamente";
            this.btnRepetir.UseVisualStyleBackColor = true;
            this.btnRepetir.Visible = false;
            this.btnRepetir.Click += new System.EventHandler(this.btnRepetir_Click);
            // 
            // txtxMotivo
            // 
            this.txtxMotivo.BackColor = System.Drawing.Color.White;
            this.txtxMotivo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtxMotivo.Location = new System.Drawing.Point(7, 100);
            this.txtxMotivo.Name = "txtxMotivo";
            this.txtxMotivo.ReadOnly = true;
            this.txtxMotivo.Size = new System.Drawing.Size(565, 39);
            this.txtxMotivo.TabIndex = 9;
            this.txtxMotivo.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Zip.Nfe.Properties.Resources.remove_16;
            this.btnClose.Location = new System.Drawing.Point(562, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(18, 16);
            this.btnClose.TabIndex = 8;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Location = new System.Drawing.Point(468, 69);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(106, 25);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(11, 70);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(451, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // lbStatus
            // 
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(11, 44);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(369, 23);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(486, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Aguarde a autorização de sua Nota Fiscal Consumidor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmEnviarNfce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 152);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEnviarNfce";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmEnviarNfe";
            this.Load += new System.EventHandler(this.FrmEnviarNfe_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRepetir;
        private System.Windows.Forms.RichTextBox txtxMotivo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label2;
    }
}