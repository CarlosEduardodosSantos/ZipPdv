namespace Toch
{
    partial class FrmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlMesas = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlFechamentos = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtMesasBloqueadas = new System.Windows.Forms.ToolStripButton();
            this.txtMesasOcupadas = new System.Windows.Forms.ToolStripButton();
            this.txtMesasDisponiveis = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.btnChamaTecladoNumerico = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTransfereMesa = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogin = new System.Windows.Forms.ToolStripButton();
            this.pnlTransferencia = new System.Windows.Forms.Panel();
            this.btnCancelarTransf = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.pnlDestino = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlOrigem = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlTransferencia.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 562);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlTransferencia);
            this.splitContainer1.Size = new System.Drawing.Size(864, 562);
            this.splitContainer1.SplitterDistance = 488;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(864, 430);
            this.splitContainer2.SplitterDistance = 352;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.pnlMesas);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(862, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mapa de Situação";
            // 
            // pnlMesas
            // 
            this.pnlMesas.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.pnlMesas.AutoScroll = true;
            this.pnlMesas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMesas.Location = new System.Drawing.Point(3, 16);
            this.pnlMesas.Name = "pnlMesas";
            this.pnlMesas.Size = new System.Drawing.Size(856, 331);
            this.pnlMesas.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlFechamentos);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(862, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sugestão de Mesas a Serem Atendidas";
            // 
            // pnlFechamentos
            // 
            this.pnlFechamentos.AutoScroll = true;
            this.pnlFechamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFechamentos.Location = new System.Drawing.Point(3, 17);
            this.pnlFechamentos.Name = "pnlFechamentos";
            this.pnlFechamentos.Size = new System.Drawing.Size(856, 52);
            this.pnlFechamentos.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtMesasBloqueadas,
            this.txtMesasOcupadas,
            this.txtMesasDisponiveis,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.btnChamaTecladoNumerico,
            this.toolStripSeparator2,
            this.btnTransfereMesa,
            this.toolStripSeparator1,
            this.btnLogin});
            this.toolStrip1.Location = new System.Drawing.Point(0, 430);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(864, 58);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtMesasBloqueadas
            // 
            this.txtMesasBloqueadas.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtMesasBloqueadas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtMesasBloqueadas.Image = ((System.Drawing.Image)(resources.GetObject("txtMesasBloqueadas.Image")));
            this.txtMesasBloqueadas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMesasBloqueadas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.txtMesasBloqueadas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtMesasBloqueadas.Name = "txtMesasBloqueadas";
            this.txtMesasBloqueadas.Size = new System.Drawing.Size(83, 55);
            this.txtMesasBloqueadas.Text = "Bloqueado";
            // 
            // txtMesasOcupadas
            // 
            this.txtMesasOcupadas.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtMesasOcupadas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtMesasOcupadas.Image = ((System.Drawing.Image)(resources.GetObject("txtMesasOcupadas.Image")));
            this.txtMesasOcupadas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMesasOcupadas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.txtMesasOcupadas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtMesasOcupadas.Name = "txtMesasOcupadas";
            this.txtMesasOcupadas.Size = new System.Drawing.Size(73, 55);
            this.txtMesasOcupadas.Text = "Ocupado";
            // 
            // txtMesasDisponiveis
            // 
            this.txtMesasDisponiveis.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtMesasDisponiveis.BackColor = System.Drawing.Color.Transparent;
            this.txtMesasDisponiveis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtMesasDisponiveis.Image = global::Toch.Properties.Resources.greenColor;
            this.txtMesasDisponiveis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMesasDisponiveis.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.txtMesasDisponiveis.ImageTransparentColor = System.Drawing.Color.Green;
            this.txtMesasDisponiveis.Name = "txtMesasDisponiveis";
            this.txtMesasDisponiveis.Size = new System.Drawing.Size(82, 55);
            this.txtMesasDisponiveis.Text = "Disponível";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 55);
            this.toolStripLabel1.Text = "Mesa >";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(110, 58);
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolStripTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // btnChamaTecladoNumerico
            // 
            this.btnChamaTecladoNumerico.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnChamaTecladoNumerico.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChamaTecladoNumerico.Image = global::Toch.Properties.Resources.icoTeclado;
            this.btnChamaTecladoNumerico.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChamaTecladoNumerico.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnChamaTecladoNumerico.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChamaTecladoNumerico.Name = "btnChamaTecladoNumerico";
            this.btnChamaTecladoNumerico.Size = new System.Drawing.Size(94, 55);
            this.btnChamaTecladoNumerico.Text = "Teclado Virtual";
            this.btnChamaTecladoNumerico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChamaTecladoNumerico.Click += new System.EventHandler(this.btnChamaTecladoNumerico_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 58);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnTransfereMesa
            // 
            this.btnTransfereMesa.Enabled = false;
            this.btnTransfereMesa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTransfereMesa.Image = global::Toch.Properties.Resources._1339161281_Transfer1;
            this.btnTransfereMesa.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTransfereMesa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTransfereMesa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTransfereMesa.Name = "btnTransfereMesa";
            this.btnTransfereMesa.Size = new System.Drawing.Size(87, 55);
            this.btnTransfereMesa.Text = "Transferencia";
            this.btnTransfereMesa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTransfereMesa.Visible = false;
            this.btnTransfereMesa.Click += new System.EventHandler(this.btnTransfereMesa_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 58);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Image = global::Toch.Properties.Resources.User;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(41, 55);
            this.btnLogin.Text = "Login";
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pnlTransferencia
            // 
            this.pnlTransferencia.Controls.Add(this.btnCancelarTransf);
            this.pnlTransferencia.Controls.Add(this.btnConfirmar);
            this.pnlTransferencia.Controls.Add(this.pnlDestino);
            this.pnlTransferencia.Controls.Add(this.label2);
            this.pnlTransferencia.Controls.Add(this.pnlOrigem);
            this.pnlTransferencia.Controls.Add(this.label1);
            this.pnlTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransferencia.Location = new System.Drawing.Point(0, 0);
            this.pnlTransferencia.Name = "pnlTransferencia";
            this.pnlTransferencia.Size = new System.Drawing.Size(864, 70);
            this.pnlTransferencia.TabIndex = 0;
            // 
            // btnCancelarTransf
            // 
            this.btnCancelarTransf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelarTransf.BackColor = System.Drawing.Color.White;
            this.btnCancelarTransf.BackgroundImage = global::Toch.Properties.Resources.Falha;
            this.btnCancelarTransf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelarTransf.FlatAppearance.BorderSize = 4;
            this.btnCancelarTransf.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarTransf.Location = new System.Drawing.Point(778, 3);
            this.btnCancelarTransf.Name = "btnCancelarTransf";
            this.btnCancelarTransf.Size = new System.Drawing.Size(83, 64);
            this.btnCancelarTransf.TabIndex = 6;
            this.btnCancelarTransf.UseVisualStyleBackColor = false;
            this.btnCancelarTransf.Click += new System.EventHandler(this.btnCancelarTransf_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirmar.BackColor = System.Drawing.Color.White;
            this.btnConfirmar.BackgroundImage = global::Toch.Properties.Resources.Correto;
            this.btnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConfirmar.FlatAppearance.BorderSize = 4;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(689, 3);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(83, 64);
            this.btnConfirmar.TabIndex = 5;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // pnlDestino
            // 
            this.pnlDestino.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnlDestino.Location = new System.Drawing.Point(444, 3);
            this.pnlDestino.Name = "pnlDestino";
            this.pnlDestino.Size = new System.Drawing.Size(143, 64);
            this.pnlDestino.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(302, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 63);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mesa Destino";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlOrigem
            // 
            this.pnlOrigem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnlOrigem.Location = new System.Drawing.Point(153, 3);
            this.pnlOrigem.Name = "pnlOrigem";
            this.pnlOrigem.Size = new System.Drawing.Size(143, 64);
            this.pnlOrigem.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mesa Origem";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 562);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPrincipal";
            this.Text = "E-Ticket Mesa Touch";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.Resize += new System.EventHandler(this.FrmPrincipal_Resize);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlTransferencia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton txtMesasBloqueadas;
        private System.Windows.Forms.ToolStripButton txtMesasOcupadas;
        private System.Windows.Forms.ToolStripButton txtMesasDisponiveis;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btnTransfereMesa;
        private System.Windows.Forms.Panel pnlTransferencia;
        private System.Windows.Forms.Panel pnlDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlOrigem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelarTransf;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlFechamentos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlMesas;
        private System.Windows.Forms.ToolStripButton btnChamaTecladoNumerico;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnLogin;

    }
}