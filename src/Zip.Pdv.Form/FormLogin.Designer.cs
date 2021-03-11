namespace Zip.Pdv
{
    partial class FormLogin
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
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flpUsuarios = new System.Windows.Forms.FlowLayoutPanel();
            this.lbTextUsuario = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.keyboardNum1 = new KeyboardClassLibrary.Num.KeyboardNum();
            this.pnlPaginacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPaginacao
            // 
            this.pnlPaginacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlPaginacao.Controls.Add(this.btnPrevious);
            this.pnlPaginacao.Controls.Add(this.btnNext);
            this.pnlPaginacao.Location = new System.Drawing.Point(12, 394);
            this.pnlPaginacao.Name = "pnlPaginacao";
            this.pnlPaginacao.Size = new System.Drawing.Size(259, 37);
            this.pnlPaginacao.TabIndex = 11;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = global::Zip.Pdv.Properties.Resources.previous_resultset_icone_6623_32;
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.Enabled = false;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Location = new System.Drawing.Point(3, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(41, 33);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::Zip.Pdv.Properties.Resources.next_resultset_icone_3882_32;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Enabled = false;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(213, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(41, 33);
            this.btnNext.TabIndex = 0;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Selecione seu usuário";
            // 
            // flpUsuarios
            // 
            this.flpUsuarios.BackColor = System.Drawing.Color.Transparent;
            this.flpUsuarios.Location = new System.Drawing.Point(12, 122);
            this.flpUsuarios.Name = "flpUsuarios";
            this.flpUsuarios.Size = new System.Drawing.Size(259, 267);
            this.flpUsuarios.TabIndex = 9;
            // 
            // lbTextUsuario
            // 
            this.lbTextUsuario.AutoSize = true;
            this.lbTextUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lbTextUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextUsuario.ForeColor = System.Drawing.Color.Black;
            this.lbTextUsuario.Location = new System.Drawing.Point(286, 97);
            this.lbTextUsuario.Name = "lbTextUsuario";
            this.lbTextUsuario.Size = new System.Drawing.Size(56, 20);
            this.lbTextUsuario.TabIndex = 8;
            this.lbTextUsuario.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSenha.BackColor = System.Drawing.Color.White;
            this.txtSenha.Enabled = false;
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(290, 122);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(282, 41);
            this.txtSenha.TabIndex = 7;
            this.txtSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenha_KeyDown);
            this.txtSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenha_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Zip.Pdv.Properties.Resources.LOGO_ORIGINAL_220;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 89);
            this.panel1.TabIndex = 12;
            // 
            // keyboardNum1
            // 
            this.keyboardNum1.Enabled = false;
            this.keyboardNum1.Location = new System.Drawing.Point(290, 169);
            this.keyboardNum1.Name = "keyboardNum1";
            this.keyboardNum1.Size = new System.Drawing.Size(282, 260);
            this.keyboardNum1.TabIndex = 13;
            this.keyboardNum1.UserKeyPressed += new KeyboardClassLibrary.Num.KeyboardNumDelegate(this.keyboardNum1_UserKeyPressed);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.keyboardNum1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlPaginacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpUsuarios);
            this.Controls.Add(this.lbTextUsuario);
            this.Controls.Add(this.txtSenha);
            this.Name = "FormLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acesso ao sistema";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.pnlPaginacao.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaginacao;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpUsuarios;
        private System.Windows.Forms.Label lbTextUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Panel panel1;
        private KeyboardClassLibrary.Num.KeyboardNum keyboardNum1;
    }
}