namespace Zip.Pdv
{
    partial class FormMesaDetalhes
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
            this.panelLista = new System.Windows.Forms.Panel();
            this.dtGridMesa = new System.Windows.Forms.DataGridView();
            this.lblMesa = new System.Windows.Forms.Label();
            this.btnLancar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTotalizador = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lbAdicionais = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lbSubTotal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbDesconto = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbValorTotal = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lbQtdeProduto = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnVenda = new System.Windows.Forms.Button();
            this.btnFecharMesa = new System.Windows.Forms.Button();
            this.btnParcelado = new System.Windows.Forms.Button();
            this.btnBonificarMesa = new System.Windows.Forms.Button();
            this.btnParcialItem = new System.Windows.Forms.Button();
            this.btnGarcom = new System.Windows.Forms.Button();
            this.btnTransferir = new System.Windows.Forms.Button();
            this.btnServico = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTexto = new System.Windows.Forms.Label();
            this.panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMesa)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelTotalizador.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLista
            // 
            this.panelLista.AutoScroll = true;
            this.panelLista.Controls.Add(this.dtGridMesa);
            this.panelLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLista.Location = new System.Drawing.Point(0, 0);
            this.panelLista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLista.Name = "panelLista";
            this.panelLista.Size = new System.Drawing.Size(1109, 950);
            this.panelLista.TabIndex = 0;
            // 
            // dtGridMesa
            // 
            this.dtGridMesa.AllowUserToAddRows = false;
            this.dtGridMesa.AllowUserToDeleteRows = false;
            this.dtGridMesa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridMesa.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtGridMesa.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtGridMesa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridMesa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridMesa.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dtGridMesa.Location = new System.Drawing.Point(0, 0);
            this.dtGridMesa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtGridMesa.MultiSelect = false;
            this.dtGridMesa.Name = "dtGridMesa";
            this.dtGridMesa.ReadOnly = true;
            this.dtGridMesa.RowHeadersWidth = 51;
            this.dtGridMesa.RowTemplate.Height = 24;
            this.dtGridMesa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridMesa.Size = new System.Drawing.Size(1109, 950);
            this.dtGridMesa.TabIndex = 0;
            this.dtGridMesa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMesa_CellClick);
            this.dtGridMesa.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtGridMesa_CellFormatting);
            this.dtGridMesa.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesa.Location = new System.Drawing.Point(17, 23);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(85, 29);
            this.lblMesa.TabIndex = 1;
            this.lblMesa.Text = "label1";
            // 
            // btnLancar
            // 
            this.btnLancar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnLancar.Enabled = false;
            this.btnLancar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLancar.Location = new System.Drawing.Point(116, 126);
            this.btnLancar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLancar.Name = "btnLancar";
            this.btnLancar.Size = new System.Drawing.Size(303, 65);
            this.btnLancar.TabIndex = 2;
            this.btnLancar.Text = "Lançar Produtos";
            this.btnLancar.UseVisualStyleBackColor = false;
            this.btnLancar.Click += new System.EventHandler(this.btnLancar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(351, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(85, 29);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panelTotalizador);
            this.panel1.Controls.Add(this.btnLimpar);
            this.panel1.Controls.Add(this.btnVenda);
            this.panel1.Controls.Add(this.btnFecharMesa);
            this.panel1.Controls.Add(this.btnParcelado);
            this.panel1.Controls.Add(this.btnBonificarMesa);
            this.panel1.Controls.Add(this.btnLancar);
            this.panel1.Controls.Add(this.btnParcialItem);
            this.panel1.Controls.Add(this.btnGarcom);
            this.panel1.Controls.Add(this.btnTransferir);
            this.panel1.Controls.Add(this.btnServico);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1109, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 950);
            this.panel1.TabIndex = 5;
            // 
            // panelTotalizador
            // 
            this.panelTotalizador.Controls.Add(this.panel10);
            this.panelTotalizador.Controls.Add(this.panel11);
            this.panelTotalizador.Controls.Add(this.panel12);
            this.panelTotalizador.Controls.Add(this.panel3);
            this.panelTotalizador.Controls.Add(this.panel13);
            this.panelTotalizador.Controls.Add(this.panel4);
            this.panelTotalizador.Controls.Add(this.panel5);
            this.panelTotalizador.Controls.Add(this.panel15);
            this.panelTotalizador.Controls.Add(this.panel14);
            this.panelTotalizador.Controls.Add(this.panel16);
            this.panelTotalizador.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotalizador.Location = new System.Drawing.Point(0, 728);
            this.panelTotalizador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTotalizador.Name = "panelTotalizador";
            this.panelTotalizador.Size = new System.Drawing.Size(524, 220);
            this.panelTotalizador.TabIndex = 44;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.Info;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.lbAdicionais);
            this.panel10.Location = new System.Drawing.Point(407, 107);
            this.panel10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(127, 50);
            this.panel10.TabIndex = 36;
            // 
            // lbAdicionais
            // 
            this.lbAdicionais.AutoSize = true;
            this.lbAdicionais.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdicionais.Location = new System.Drawing.Point(-3, 14);
            this.lbAdicionais.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.panel11.Location = new System.Drawing.Point(277, 107);
            this.panel11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(147, 50);
            this.panel11.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Turquoise;
            this.label10.Location = new System.Drawing.Point(39, 14);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 20);
            this.label10.TabIndex = 32;
            this.label10.Text = "Taxa:";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.SystemColors.Info;
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.lbSubTotal);
            this.panel12.Location = new System.Drawing.Point(407, 62);
            this.panel12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(127, 50);
            this.panel12.TabIndex = 40;
            // 
            // lbSubTotal
            // 
            this.lbSubTotal.AutoSize = true;
            this.lbSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubTotal.Location = new System.Drawing.Point(-3, 9);
            this.lbSubTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSubTotal.Name = "lbSubTotal";
            this.lbSubTotal.Size = new System.Drawing.Size(73, 20);
            this.lbSubTotal.TabIndex = 31;
            this.lbSubTotal.Text = "R$ 0,00";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbDesconto);
            this.panel3.Location = new System.Drawing.Point(124, 107);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(158, 50);
            this.panel3.TabIndex = 34;
            // 
            // lbDesconto
            // 
            this.lbDesconto.AutoSize = true;
            this.lbDesconto.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDesconto.Location = new System.Drawing.Point(4, 14);
            this.lbDesconto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDesconto.Name = "lbDesconto";
            this.lbDesconto.Size = new System.Drawing.Size(73, 20);
            this.lbDesconto.TabIndex = 1;
            this.lbDesconto.Text = "R$ 0,00";
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.SystemColors.Info;
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.label7);
            this.panel13.Location = new System.Drawing.Point(277, 62);
            this.panel13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(147, 50);
            this.panel13.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Subtotal:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Info;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label12);
            this.panel4.Location = new System.Drawing.Point(-1, 107);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(127, 50);
            this.panel4.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Turquoise;
            this.label12.Location = new System.Drawing.Point(12, 14);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 20);
            this.label12.TabIndex = 31;
            this.label12.Text = "Desconto:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.lbValorTotal);
            this.panel5.Location = new System.Drawing.Point(277, 134);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(251, 86);
            this.panel5.TabIndex = 32;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Info;
            this.panel6.Location = new System.Drawing.Point(300, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(292, 79);
            this.panel6.TabIndex = 21;
            // 
            // lbValorTotal
            // 
            this.lbValorTotal.AutoSize = true;
            this.lbValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorTotal.Location = new System.Drawing.Point(48, 39);
            this.lbValorTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbValorTotal.Name = "lbValorTotal";
            this.lbValorTotal.Size = new System.Drawing.Size(101, 29);
            this.lbValorTotal.TabIndex = 17;
            this.lbValorTotal.Text = "R$ 0,00";
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.SystemColors.Info;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.label5);
            this.panel15.Location = new System.Drawing.Point(-1, 62);
            this.panel15.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(127, 50);
            this.panel15.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "Qtde Itens:";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.Info;
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.lbQtdeProduto);
            this.panel14.Location = new System.Drawing.Point(124, 62);
            this.panel14.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(162, 50);
            this.panel14.TabIndex = 38;
            // 
            // lbQtdeProduto
            // 
            this.lbQtdeProduto.AutoSize = true;
            this.lbQtdeProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQtdeProduto.Location = new System.Drawing.Point(11, 9);
            this.lbQtdeProduto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbQtdeProduto.Name = "lbQtdeProduto";
            this.lbQtdeProduto.Size = new System.Drawing.Size(44, 20);
            this.lbQtdeProduto.TabIndex = 0;
            this.lbQtdeProduto.Text = "0.00";
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.panel17);
            this.panel16.Controls.Add(this.label14);
            this.panel16.Location = new System.Drawing.Point(0, 134);
            this.panel16.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(286, 86);
            this.panel16.TabIndex = 31;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.SystemColors.Info;
            this.panel17.Location = new System.Drawing.Point(300, 0);
            this.panel17.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(292, 79);
            this.panel17.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(17, 39);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 29);
            this.label14.TabIndex = 16;
            this.label14.Text = "Total a Pagar:";
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.Maroon;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLimpar.Location = new System.Drawing.Point(116, 673);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(303, 54);
            this.btnLimpar.TabIndex = 15;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnVenda
            // 
            this.btnVenda.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnVenda.Enabled = false;
            this.btnVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVenda.Location = new System.Drawing.Point(116, 613);
            this.btnVenda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVenda.Name = "btnVenda";
            this.btnVenda.Size = new System.Drawing.Size(303, 54);
            this.btnVenda.TabIndex = 9;
            this.btnVenda.Text = "Finalizar Venda";
            this.btnVenda.UseVisualStyleBackColor = false;
            this.btnVenda.Click += new System.EventHandler(this.btnVenda_Click);
            // 
            // btnFecharMesa
            // 
            this.btnFecharMesa.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFecharMesa.Enabled = false;
            this.btnFecharMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharMesa.Location = new System.Drawing.Point(116, 502);
            this.btnFecharMesa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFecharMesa.Name = "btnFecharMesa";
            this.btnFecharMesa.Size = new System.Drawing.Size(303, 49);
            this.btnFecharMesa.TabIndex = 7;
            this.btnFecharMesa.Text = "Fechar Mesa";
            this.btnFecharMesa.UseVisualStyleBackColor = false;
            this.btnFecharMesa.Click += new System.EventHandler(this.btnFecharMesa_Click);
            // 
            // btnParcelado
            // 
            this.btnParcelado.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnParcelado.Enabled = false;
            this.btnParcelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParcelado.Location = new System.Drawing.Point(116, 389);
            this.btnParcelado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnParcelado.Name = "btnParcelado";
            this.btnParcelado.Size = new System.Drawing.Size(303, 49);
            this.btnParcelado.TabIndex = 14;
            this.btnParcelado.Text = "Parcelado";
            this.btnParcelado.UseVisualStyleBackColor = false;
            this.btnParcelado.Click += new System.EventHandler(this.btnParcelado_Click);
            // 
            // btnBonificarMesa
            // 
            this.btnBonificarMesa.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnBonificarMesa.Enabled = false;
            this.btnBonificarMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBonificarMesa.Location = new System.Drawing.Point(116, 446);
            this.btnBonificarMesa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBonificarMesa.Name = "btnBonificarMesa";
            this.btnBonificarMesa.Size = new System.Drawing.Size(303, 50);
            this.btnBonificarMesa.TabIndex = 13;
            this.btnBonificarMesa.Text = "Bonificar Mesa";
            this.btnBonificarMesa.UseVisualStyleBackColor = false;
            this.btnBonificarMesa.Click += new System.EventHandler(this.btnBonificarMesa_Click);
            // 
            // btnParcialItem
            // 
            this.btnParcialItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnParcialItem.Enabled = false;
            this.btnParcialItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParcialItem.Location = new System.Drawing.Point(116, 558);
            this.btnParcialItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnParcialItem.Name = "btnParcialItem";
            this.btnParcialItem.Size = new System.Drawing.Size(303, 49);
            this.btnParcialItem.TabIndex = 16;
            this.btnParcialItem.Text = "Individuais";
            this.btnParcialItem.UseVisualStyleBackColor = false;
            this.btnParcialItem.Click += new System.EventHandler(this.btnParcialItem_Click);
            // 
            // btnGarcom
            // 
            this.btnGarcom.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnGarcom.Enabled = false;
            this.btnGarcom.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGarcom.Location = new System.Drawing.Point(116, 326);
            this.btnGarcom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGarcom.Name = "btnGarcom";
            this.btnGarcom.Size = new System.Drawing.Size(303, 55);
            this.btnGarcom.TabIndex = 11;
            this.btnGarcom.Text = "Alterar Garçom";
            this.btnGarcom.UseVisualStyleBackColor = false;
            this.btnGarcom.Click += new System.EventHandler(this.btnGarcom_Click);
            // 
            // btnTransferir
            // 
            this.btnTransferir.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnTransferir.Enabled = false;
            this.btnTransferir.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferir.Location = new System.Drawing.Point(116, 261);
            this.btnTransferir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(303, 58);
            this.btnTransferir.TabIndex = 10;
            this.btnTransferir.Text = "Transferir Mesa";
            this.btnTransferir.UseVisualStyleBackColor = false;
            this.btnTransferir.Click += new System.EventHandler(this.btnTransferir_Click);
            // 
            // btnServico
            // 
            this.btnServico.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnServico.Enabled = false;
            this.btnServico.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServico.Location = new System.Drawing.Point(116, 196);
            this.btnServico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnServico.Name = "btnServico";
            this.btnServico.Size = new System.Drawing.Size(303, 59);
            this.btnServico.TabIndex = 8;
            this.btnServico.Text = "Incluir Serviço";
            this.btnServico.UseVisualStyleBackColor = false;
            this.btnServico.Click += new System.EventHandler(this.btnServico_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTexto);
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.lblMesa);
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 78);
            this.panel2.TabIndex = 6;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(272, 36);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(47, 16);
            this.lblTexto.TabIndex = 5;
            this.lblTexto.Text = "Status:";
            // 
            // FormMesaDetalhes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 950);
            this.Controls.Add(this.panelLista);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMesaDetalhes";
            this.ShowIcon = false;
            this.Text = "Detalhes da Mesa";
            this.Activated += new System.EventHandler(this.FormMesaDetalhes_Activated);
            this.Load += new System.EventHandler(this.FormMesaDetalhes_Load);
            this.panelLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMesa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelTotalizador.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLista;
        private System.Windows.Forms.DataGridView dtGridMesa;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.Button btnLancar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.Button btnFecharMesa;
        private System.Windows.Forms.Button btnServico;
        private System.Windows.Forms.Button btnVenda;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Button btnGarcom;
        private System.Windows.Forms.Button btnBonificarMesa;
        private System.Windows.Forms.Button btnParcelado;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnParcialItem;
        private System.Windows.Forms.Panel panelTotalizador;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lbAdicionais;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lbSubTotal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbDesconto;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbValorTotal;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label lbQtdeProduto;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label14;
    }
}