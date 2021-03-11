namespace Zip.Pdv
{
    partial class FormDesconto
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
            this.panel23 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.keyboardNum1 = new KeyboardClassLibrary.Num.KeyboardNum();
            this.txtValor = new Zip.Pdv.Component.TextBoxDecimal();
            this.btnDescontoReal = new System.Windows.Forms.Button();
            this.btnDescontoPercentual = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel23.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDescontoPercentual);
            this.panel1.Controls.Add(this.btnDescontoReal);
            this.panel1.Controls.Add(this.keyboardNum1);
            this.panel1.Controls.Add(this.txtValor);
            this.panel1.Controls.Add(this.panel23);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 431);
            this.panel1.TabIndex = 0;
            // 
            // panel23
            // 
            this.panel23.BackgroundImage = global::Zip.Pdv.Properties.Resources.bg_;
            this.panel23.Controls.Add(this.label2);
            this.panel23.Controls.Add(this.btnVoltar);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(316, 42);
            this.panel23.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(72, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "DESCONTO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.Transparent;
            this.btnVoltar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVoltar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Image = global::Zip.Pdv.Properties.Resources.back_32;
            this.btnVoltar.Location = new System.Drawing.Point(0, 0);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(50, 42);
            this.btnVoltar.TabIndex = 4;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // keyboardNum1
            // 
            this.keyboardNum1.Location = new System.Drawing.Point(11, 177);
            this.keyboardNum1.Name = "keyboardNum1";
            this.keyboardNum1.Size = new System.Drawing.Size(293, 246);
            this.keyboardNum1.TabIndex = 163;
            this.keyboardNum1.UserKeyPressed += new KeyboardClassLibrary.Num.KeyboardNumDelegate(this.keyboardcontrol1_UserKeyPressed);
            // 
            // txtValor
            // 
            this.txtValor.BackColorEnter = System.Drawing.Color.Empty;
            this.txtValor.CasasDecimais = null;
            this.txtValor.Font = new System.Drawing.Font("Arial", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.FormatDecimal = null;
            this.txtValor.Location = new System.Drawing.Point(10, 126);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(294, 45);
            this.txtValor.TabIndex = 162;
            this.txtValor.Text = "0";
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValor.ValueNumeric = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnDescontoReal
            // 
            this.btnDescontoReal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(166)))), ((int)(((byte)(222)))));
            this.btnDescontoReal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescontoReal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescontoReal.ForeColor = System.Drawing.Color.White;
            this.btnDescontoReal.Location = new System.Drawing.Point(11, 48);
            this.btnDescontoReal.Name = "btnDescontoReal";
            this.btnDescontoReal.Size = new System.Drawing.Size(145, 72);
            this.btnDescontoReal.TabIndex = 164;
            this.btnDescontoReal.Text = "Desconto R$";
            this.btnDescontoReal.UseVisualStyleBackColor = false;
            this.btnDescontoReal.Click += new System.EventHandler(this.btnDescontoReal_Click);
            // 
            // btnDescontoPercentual
            // 
            this.btnDescontoPercentual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(166)))), ((int)(((byte)(222)))));
            this.btnDescontoPercentual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescontoPercentual.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescontoPercentual.ForeColor = System.Drawing.Color.White;
            this.btnDescontoPercentual.Location = new System.Drawing.Point(159, 48);
            this.btnDescontoPercentual.Name = "btnDescontoPercentual";
            this.btnDescontoPercentual.Size = new System.Drawing.Size(145, 72);
            this.btnDescontoPercentual.TabIndex = 165;
            this.btnDescontoPercentual.Text = "Desconto %";
            this.btnDescontoPercentual.UseVisualStyleBackColor = false;
            this.btnDescontoPercentual.Click += new System.EventHandler(this.btnDescontoPercentual_Click);
            // 
            // FormDesconto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 431);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDesconto";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDesconto";
            this.Load += new System.EventHandler(this.FormDesconto_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnDescontoPercentual;
        private System.Windows.Forms.Button btnDescontoReal;
        private KeyboardClassLibrary.Num.KeyboardNum keyboardNum1;
        private Component.TextBoxDecimal txtValor;
    }
}