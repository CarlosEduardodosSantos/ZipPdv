namespace Zip.Pdv.Component.CupomGrid
{
    partial class CupomItem
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbDesconto = new System.Windows.Forms.Label();
            this.lbValorTotal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbQuantidade = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbValorUnit = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbProduto = new System.Windows.Forms.Label();
            this.imageProduto = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnTaskItem = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.lbPrecoDe = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageProduto)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTaskItem)).BeginInit();
            this.panelPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(480, 49);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbDesconto);
            this.panel4.Controls.Add(this.lbValorTotal);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(363, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(90, 43);
            this.panel4.TabIndex = 3;
            // 
            // lbDesconto
            // 
            this.lbDesconto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDesconto.BackColor = System.Drawing.Color.Transparent;
            this.lbDesconto.ForeColor = System.Drawing.Color.Red;
            this.lbDesconto.Location = new System.Drawing.Point(4, 2);
            this.lbDesconto.Name = "lbDesconto";
            this.lbDesconto.Size = new System.Drawing.Size(85, 13);
            this.lbDesconto.TabIndex = 2;
            this.lbDesconto.Text = "0";
            this.lbDesconto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbValorTotal
            // 
            this.lbValorTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbValorTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorTotal.Location = new System.Drawing.Point(0, 0);
            this.lbValorTotal.Name = "lbValorTotal";
            this.lbValorTotal.Size = new System.Drawing.Size(90, 43);
            this.lbValorTotal.TabIndex = 1;
            this.lbValorTotal.Text = "label1";
            this.lbValorTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbValorTotal.Click += new System.EventHandler(this.selectItem);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbQuantidade);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(291, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(66, 43);
            this.panel3.TabIndex = 2;
            // 
            // lbQuantidade
            // 
            this.lbQuantidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbQuantidade.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuantidade.Location = new System.Drawing.Point(0, 0);
            this.lbQuantidade.Name = "lbQuantidade";
            this.lbQuantidade.Size = new System.Drawing.Size(66, 43);
            this.lbQuantidade.TabIndex = 1;
            this.lbQuantidade.Text = "label1";
            this.lbQuantidade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbQuantidade.Click += new System.EventHandler(this.selectItem);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbPrecoDe);
            this.panel2.Controls.Add(this.lbValorUnit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(195, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(90, 43);
            this.panel2.TabIndex = 1;
            // 
            // lbValorUnit
            // 
            this.lbValorUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbValorUnit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorUnit.Location = new System.Drawing.Point(0, 0);
            this.lbValorUnit.Name = "lbValorUnit";
            this.lbValorUnit.Size = new System.Drawing.Size(90, 43);
            this.lbValorUnit.TabIndex = 1;
            this.lbValorUnit.Text = "label1";
            this.lbValorUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbValorUnit.Click += new System.EventHandler(this.selectItem);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbProduto);
            this.panel1.Controls.Add(this.imageProduto);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 43);
            this.panel1.TabIndex = 0;
            // 
            // lbProduto
            // 
            this.lbProduto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProduto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProduto.Location = new System.Drawing.Point(48, 0);
            this.lbProduto.Name = "lbProduto";
            this.lbProduto.Size = new System.Drawing.Size(138, 43);
            this.lbProduto.TabIndex = 0;
            this.lbProduto.Text = "label1";
            this.lbProduto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbProduto.Click += new System.EventHandler(this.selectItem);
            // 
            // imageProduto
            // 
            this.imageProduto.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageProduto.Location = new System.Drawing.Point(0, 0);
            this.imageProduto.Name = "imageProduto";
            this.imageProduto.Size = new System.Drawing.Size(48, 43);
            this.imageProduto.TabIndex = 2;
            this.imageProduto.TabStop = false;
            this.imageProduto.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnTaskItem);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(459, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(18, 43);
            this.panel5.TabIndex = 4;
            // 
            // btnTaskItem
            // 
            this.btnTaskItem.BackgroundImage = global::Zip.Pdv.Properties.Resources.Trash_16;
            this.btnTaskItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTaskItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTaskItem.ImageLocation = "";
            this.btnTaskItem.Location = new System.Drawing.Point(0, 0);
            this.btnTaskItem.Name = "btnTaskItem";
            this.btnTaskItem.Size = new System.Drawing.Size(18, 43);
            this.btnTaskItem.TabIndex = 0;
            this.btnTaskItem.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 49);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(480, 1);
            this.panel6.TabIndex = 1;
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Controls.Add(this.tableLayoutPanel1);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(480, 49);
            this.panelPrincipal.TabIndex = 2;
            // 
            // lbPrecoDe
            // 
            this.lbPrecoDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPrecoDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecoDe.ForeColor = System.Drawing.Color.Red;
            this.lbPrecoDe.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbPrecoDe.Location = new System.Drawing.Point(3, 0);
            this.lbPrecoDe.Name = "lbPrecoDe";
            this.lbPrecoDe.Size = new System.Drawing.Size(84, 13);
            this.lbPrecoDe.TabIndex = 3;
            this.lbPrecoDe.Text = "0";
            this.lbPrecoDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CupomItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panel6);
            this.Name = "CupomItem";
            this.Size = new System.Drawing.Size(480, 50);
            this.Load += new System.EventHandler(this.CupomItem_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageProduto)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnTaskItem)).EndInit();
            this.panelPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbValorTotal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbQuantidade;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbValorUnit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbProduto;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox btnTaskItem;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbDesconto;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.PictureBox imageProduto;
        private System.Windows.Forms.Label lbPrecoDe;
    }
}
