namespace Zip.Pdv.NFce
{
    partial class FrmNfceNaoEnviada
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvVendas = new System.Windows.Forms.DataGridView();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPesquisaVenda = new System.Windows.Forms.Button();
            this.clInOperacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTipoOperacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDesconto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clHistorico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPesquisaVenda);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpInicio);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 397);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(655, 53);
            this.panel2.TabIndex = 1;
            // 
            // dgvVendas
            // 
            this.dgvVendas.AllowUserToAddRows = false;
            this.dgvVendas.AllowUserToDeleteRows = false;
            this.dgvVendas.AllowUserToResizeColumns = false;
            this.dgvVendas.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvVendas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVendas.BackgroundColor = System.Drawing.Color.White;
            this.dgvVendas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVendas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvVendas.ColumnHeadersHeight = 30;
            this.dgvVendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvVendas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clInOperacao,
            this.clTipoOperacao,
            this.clTipo,
            this.clDesconto,
            this.clHistorico});
            this.dgvVendas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVendas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvVendas.Location = new System.Drawing.Point(0, 0);
            this.dgvVendas.MultiSelect = false;
            this.dgvVendas.Name = "dgvVendas";
            this.dgvVendas.ReadOnly = true;
            this.dgvVendas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvVendas.RowHeadersVisible = false;
            this.dgvVendas.RowHeadersWidth = 30;
            this.dgvVendas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVendas.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvVendas.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvVendas.RowTemplate.Height = 25;
            this.dgvVendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendas.ShowCellErrors = false;
            this.dgvVendas.ShowCellToolTips = false;
            this.dgvVendas.ShowEditingIcon = false;
            this.dgvVendas.ShowRowErrors = false;
            this.dgvVendas.Size = new System.Drawing.Size(655, 397);
            this.dgvVendas.TabIndex = 7;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Checked = false;
            this.dtpInicio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(137, 12);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(155, 29);
            this.dtpInicio.TabIndex = 16;
            this.dtpInicio.ValueChanged += new System.EventHandler(this.dtpInicio_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 17;
            this.label1.Text = "Data referência";
            // 
            // btnPesquisaVenda
            // 
            this.btnPesquisaVenda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisaVenda.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnPesquisaVenda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisaVenda.Location = new System.Drawing.Point(551, 6);
            this.btnPesquisaVenda.Name = "btnPesquisaVenda";
            this.btnPesquisaVenda.Size = new System.Drawing.Size(92, 39);
            this.btnPesquisaVenda.TabIndex = 19;
            this.btnPesquisaVenda.Text = "GERAR ";
            this.btnPesquisaVenda.UseVisualStyleBackColor = true;
            this.btnPesquisaVenda.Click += new System.EventHandler(this.btnPesquisaVenda_Click);
            // 
            // clInOperacao
            // 
            this.clInOperacao.DataPropertyName = "VendaId";
            this.clInOperacao.Frozen = true;
            this.clInOperacao.HeaderText = "Nº. VENDA";
            this.clInOperacao.Name = "clInOperacao";
            this.clInOperacao.ReadOnly = true;
            this.clInOperacao.Width = 110;
            // 
            // clTipoOperacao
            // 
            this.clTipoOperacao.DataPropertyName = "NumeroNfce";
            this.clTipoOperacao.Frozen = true;
            this.clTipoOperacao.HeaderText = "Número";
            this.clTipoOperacao.Name = "clTipoOperacao";
            this.clTipoOperacao.ReadOnly = true;
            this.clTipoOperacao.Width = 110;
            // 
            // clTipo
            // 
            this.clTipo.DataPropertyName = "Serie";
            this.clTipo.Frozen = true;
            this.clTipo.HeaderText = "Série";
            this.clTipo.Name = "clTipo";
            this.clTipo.ReadOnly = true;
            this.clTipo.Width = 150;
            // 
            // clDesconto
            // 
            this.clDesconto.DataPropertyName = "Modelo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clDesconto.DefaultCellStyle = dataGridViewCellStyle5;
            this.clDesconto.Frozen = true;
            this.clDesconto.HeaderText = "Modelo";
            this.clDesconto.Name = "clDesconto";
            this.clDesconto.ReadOnly = true;
            this.clDesconto.Width = 88;
            // 
            // clHistorico
            // 
            this.clHistorico.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clHistorico.DataPropertyName = "DataHora";
            this.clHistorico.HeaderText = "Data Hora";
            this.clHistorico.Name = "clHistorico";
            this.clHistorico.ReadOnly = true;
            // 
            // FrmNfceNaoEnviada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 450);
            this.Controls.Add(this.dgvVendas);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmNfceNaoEnviada";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nfce não transmitidas";
            this.Load += new System.EventHandler(this.FrmNfceNaoEnviada_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvVendas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Button btnPesquisaVenda;
        private System.Windows.Forms.DataGridViewTextBoxColumn clInOperacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTipoOperacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDesconto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clHistorico;
    }
}