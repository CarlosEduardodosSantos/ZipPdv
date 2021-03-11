namespace Toch
{
    partial class FrmSelecionaGarcom
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExcluirItens = new System.Windows.Forms.Button();
            this.btnAdicionarQtde = new System.Windows.Forms.Button();
            this.txtQuantidadePessoas = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.cbGarcom = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 143);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExcluirItens);
            this.groupBox2.Controls.Add(this.btnAdicionarQtde);
            this.groupBox2.Controls.Add(this.txtQuantidadePessoas);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 143);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quantidades Pessoas";
            // 
            // btnExcluirItens
            // 
            this.btnExcluirItens.BackColor = System.Drawing.Color.Red;
            this.btnExcluirItens.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirItens.Location = new System.Drawing.Point(71, 84);
            this.btnExcluirItens.Name = "btnExcluirItens";
            this.btnExcluirItens.Size = new System.Drawing.Size(64, 37);
            this.btnExcluirItens.TabIndex = 3;
            this.btnExcluirItens.Text = "-";
            this.btnExcluirItens.UseVisualStyleBackColor = false;
            this.btnExcluirItens.Click += new System.EventHandler(this.btnExcluirItens_Click);
            // 
            // btnAdicionarQtde
            // 
            this.btnAdicionarQtde.BackColor = System.Drawing.Color.Green;
            this.btnAdicionarQtde.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarQtde.Location = new System.Drawing.Point(6, 84);
            this.btnAdicionarQtde.Name = "btnAdicionarQtde";
            this.btnAdicionarQtde.Size = new System.Drawing.Size(64, 37);
            this.btnAdicionarQtde.TabIndex = 2;
            this.btnAdicionarQtde.Text = "+";
            this.btnAdicionarQtde.UseVisualStyleBackColor = false;
            this.btnAdicionarQtde.Click += new System.EventHandler(this.btnAdicionarQtde_Click);
            // 
            // txtQuantidadePessoas
            // 
            this.txtQuantidadePessoas.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidadePessoas.Location = new System.Drawing.Point(6, 25);
            this.txtQuantidadePessoas.Multiline = true;
            this.txtQuantidadePessoas.Name = "txtQuantidadePessoas";
            this.txtQuantidadePessoas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtQuantidadePessoas.Size = new System.Drawing.Size(245, 33);
            this.txtQuantidadePessoas.TabIndex = 0;
            this.txtQuantidadePessoas.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConfirmar);
            this.groupBox1.Controls.Add(this.cbGarcom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(263, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleção do Garcom";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirmar.FlatAppearance.BorderSize = 4;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(12, 84);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(227, 37);
            this.btnConfirmar.TabIndex = 3;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // cbGarcom
            // 
            this.cbGarcom.AllowDrop = true;
            this.cbGarcom.DropDownHeight = 110;
            this.cbGarcom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGarcom.Enabled = false;
            this.cbGarcom.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGarcom.IntegralHeight = false;
            this.cbGarcom.ItemHeight = 25;
            this.cbGarcom.Location = new System.Drawing.Point(12, 28);
            this.cbGarcom.Name = "cbGarcom";
            this.cbGarcom.Size = new System.Drawing.Size(227, 33);
            this.cbGarcom.TabIndex = 0;
            // 
            // FrmSelecionaGarcom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 143);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelecionaGarcom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleção de Garcom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSelecionaGarcom_FormClosed);
            this.Load += new System.EventHandler(this.FrmSelecionaGarcom_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtQuantidadePessoas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbGarcom;
        private System.Windows.Forms.Button btnExcluirItens;
        private System.Windows.Forms.Button btnAdicionarQtde;
        private System.Windows.Forms.Button btnConfirmar;
    }
}