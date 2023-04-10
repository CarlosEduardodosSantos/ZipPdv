namespace Zip.Pdv
{
    partial class FormMesaPagParcial
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
            this.dtGridMesa = new System.Windows.Forms.DataGridView();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBonificar = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lbSubTotal = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lbQtdeProduto = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lbAdicionais = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbDesconto = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbValorTotal = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panelTotalizador = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMesa)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panelTotalizador.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtGridMesa
            // 
            this.dtGridMesa.AllowUserToAddRows = false;
            this.dtGridMesa.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtGridMesa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridMesa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridMesa.Location = new System.Drawing.Point(0, 0);
            this.dtGridMesa.MultiSelect = false;
            this.dtGridMesa.Name = "dtGridMesa";
            this.dtGridMesa.ReadOnly = true;
            this.dtGridMesa.RowHeadersWidth = 51;
            this.dtGridMesa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridMesa.Size = new System.Drawing.Size(1208, 578);
            this.dtGridMesa.TabIndex = 0;
            this.dtGridMesa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridMesa_CellClick);
            this.dtGridMesa.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dtGridMesa_CellPainting);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Location = new System.Drawing.Point(74, 223);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(265, 49);
            this.btnFinalizar.TabIndex = 8;
            this.btnFinalizar.Text = "Finalizar Venda Parcial";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panelTotalizador);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnBonificar);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(811, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 578);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(90, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 29);
            this.label1.TabIndex = 42;
            this.label1.Text = "Ações Individuais";
            // 
            // btnBonificar
            // 
            this.btnBonificar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnBonificar.Enabled = false;
            this.btnBonificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBonificar.Location = new System.Drawing.Point(74, 123);
            this.btnBonificar.Name = "btnBonificar";
            this.btnBonificar.Size = new System.Drawing.Size(265, 49);
            this.btnBonificar.TabIndex = 41;
            this.btnBonificar.Text = "Bonificar itens";
            this.btnBonificar.UseVisualStyleBackColor = false;
            this.btnBonificar.Click += new System.EventHandler(this.btnBonificar_Click);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.SystemColors.Info;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.label5);
            this.panel15.Location = new System.Drawing.Point(-1, 50);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(117, 41);
            this.panel15.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "Qtde Itens:";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.SystemColors.Info;
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.lbSubTotal);
            this.panel12.Location = new System.Drawing.Point(318, 50);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(83, 41);
            this.panel12.TabIndex = 40;
            // 
            // lbSubTotal
            // 
            this.lbSubTotal.AutoSize = true;
            this.lbSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubTotal.Location = new System.Drawing.Point(2, 12);
            this.lbSubTotal.Name = "lbSubTotal";
            this.lbSubTotal.Size = new System.Drawing.Size(73, 20);
            this.lbSubTotal.TabIndex = 31;
            this.lbSubTotal.Text = "R$ 0,00";
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.SystemColors.Info;
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.label7);
            this.panel13.Location = new System.Drawing.Point(211, 50);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(108, 41);
            this.panel13.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Subtotal:";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.Info;
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.lbQtdeProduto);
            this.panel14.Location = new System.Drawing.Point(111, 50);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(104, 41);
            this.panel14.TabIndex = 38;
            // 
            // lbQtdeProduto
            // 
            this.lbQtdeProduto.AutoSize = true;
            this.lbQtdeProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQtdeProduto.Location = new System.Drawing.Point(8, 7);
            this.lbQtdeProduto.Name = "lbQtdeProduto";
            this.lbQtdeProduto.Size = new System.Drawing.Size(44, 20);
            this.lbQtdeProduto.TabIndex = 0;
            this.lbQtdeProduto.Text = "0.00";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.Info;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.lbAdicionais);
            this.panel10.Location = new System.Drawing.Point(318, 87);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(83, 41);
            this.panel10.TabIndex = 36;
            // 
            // lbAdicionais
            // 
            this.lbAdicionais.AutoSize = true;
            this.lbAdicionais.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdicionais.Location = new System.Drawing.Point(2, 11);
            this.lbAdicionais.Name = "lbAdicionais";
            this.lbAdicionais.Size = new System.Drawing.Size(73, 20);
            this.lbAdicionais.TabIndex = 2;
            this.lbAdicionais.Text = "R$ 0,00";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.Info;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.label10);
            this.panel11.Location = new System.Drawing.Point(211, 87);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(108, 41);
            this.panel11.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Turquoise;
            this.label10.Location = new System.Drawing.Point(29, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 20);
            this.label10.TabIndex = 32;
            this.label10.Text = "Taxa:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbDesconto);
            this.panel2.Location = new System.Drawing.Point(115, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(97, 41);
            this.panel2.TabIndex = 34;
            // 
            // lbDesconto
            // 
            this.lbDesconto.AutoSize = true;
            this.lbDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDesconto.Location = new System.Drawing.Point(8, 13);
            this.lbDesconto.Name = "lbDesconto";
            this.lbDesconto.Size = new System.Drawing.Size(73, 20);
            this.lbDesconto.TabIndex = 1;
            this.lbDesconto.Text = "R$ 0,00";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(-1, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(117, 41);
            this.panel3.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Turquoise;
            this.label12.Location = new System.Drawing.Point(9, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 20);
            this.label12.TabIndex = 31;
            this.label12.Text = "Desconto:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.lbValorTotal);
            this.panel4.Location = new System.Drawing.Point(211, 109);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(186, 70);
            this.panel4.TabIndex = 32;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Info;
            this.panel5.Location = new System.Drawing.Point(225, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(219, 64);
            this.panel5.TabIndex = 21;
            // 
            // lbValorTotal
            // 
            this.lbValorTotal.AutoSize = true;
            this.lbValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorTotal.Location = new System.Drawing.Point(28, 21);
            this.lbValorTotal.Name = "lbValorTotal";
            this.lbValorTotal.Size = new System.Drawing.Size(101, 29);
            this.lbValorTotal.TabIndex = 17;
            this.lbValorTotal.Text = "R$ 0,00";
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.panel17);
            this.panel16.Controls.Add(this.label14);
            this.panel16.Location = new System.Drawing.Point(0, 109);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(215, 70);
            this.panel16.TabIndex = 31;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.SystemColors.Info;
            this.panel17.Location = new System.Drawing.Point(225, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(219, 64);
            this.panel17.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(13, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 29);
            this.label14.TabIndex = 16;
            this.label14.Text = "Total a Pagar:";
            // 
            // panelTotalizador
            // 
            this.panelTotalizador.Controls.Add(this.panel11);
            this.panelTotalizador.Controls.Add(this.panel10);
            this.panelTotalizador.Controls.Add(this.panel12);
            this.panelTotalizador.Controls.Add(this.panel2);
            this.panelTotalizador.Controls.Add(this.panel13);
            this.panelTotalizador.Controls.Add(this.panel3);
            this.panelTotalizador.Controls.Add(this.panel4);
            this.panelTotalizador.Controls.Add(this.panel15);
            this.panelTotalizador.Controls.Add(this.panel14);
            this.panelTotalizador.Controls.Add(this.panel16);
            this.panelTotalizador.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotalizador.Location = new System.Drawing.Point(0, 397);
            this.panelTotalizador.Name = "panelTotalizador";
            this.panelTotalizador.Size = new System.Drawing.Size(395, 179);
            this.panelTotalizador.TabIndex = 43;
            // 
            // FormMesaPagParcial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 578);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtGridMesa);
            this.Name = "FormMesaPagParcial";
            this.ShowIcon = false;
            this.Text = "Individuais";
            this.Load += new System.EventHandler(this.FormMesaPagParcial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMesa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panelTotalizador.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGridMesa;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBonificar;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lbSubTotal;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label lbQtdeProduto;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lbAdicionais;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbDesconto;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbValorTotal;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTotalizador;
    }
}