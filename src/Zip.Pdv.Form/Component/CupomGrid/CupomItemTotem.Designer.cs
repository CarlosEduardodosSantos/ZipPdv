namespace Zip.Pdv.Component.CupomGrid
{
    partial class CupomItemTotem
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbPrecoDe = new System.Windows.Forms.Label();
            this.lbValorUnit = new System.Windows.Forms.Label();
            this.lbProduto = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbQuantidade = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTaskItem = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTaskItem)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.56647F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.98434F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.35155F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.35155F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.74609F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(480, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbDesconto);
            this.panel4.Controls.Add(this.lbValorTotal);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(356, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(67, 50);
            this.panel4.TabIndex = 3;
            // 
            // lbDesconto
            // 
            this.lbDesconto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDesconto.BackColor = System.Drawing.Color.Transparent;
            this.lbDesconto.ForeColor = System.Drawing.Color.Red;
            this.lbDesconto.Location = new System.Drawing.Point(4, 6);
            this.lbDesconto.Name = "lbDesconto";
            this.lbDesconto.Size = new System.Drawing.Size(53, 13);
            this.lbDesconto.TabIndex = 2;
            this.lbDesconto.Text = "0";
            this.lbDesconto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbValorTotal
            // 
            this.lbValorTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbValorTotal.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorTotal.Location = new System.Drawing.Point(0, 0);
            this.lbValorTotal.Name = "lbValorTotal";
            this.lbValorTotal.Size = new System.Drawing.Size(67, 50);
            this.lbValorTotal.TabIndex = 1;
            this.lbValorTotal.Text = "R$ 1,00";
            this.lbValorTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbValorTotal.Click += new System.EventHandler(this.selectItem);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbProduto);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(77, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 50);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 50);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnTaskItem);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(429, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(48, 50);
            this.panel5.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 56);
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
            this.panelPrincipal.Size = new System.Drawing.Size(480, 56);
            this.panelPrincipal.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbPrecoDe);
            this.panel3.Controls.Add(this.lbValorUnit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(283, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(67, 50);
            this.panel3.TabIndex = 2;
            // 
            // lbPrecoDe
            // 
            this.lbPrecoDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPrecoDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecoDe.ForeColor = System.Drawing.Color.Red;
            this.lbPrecoDe.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbPrecoDe.Location = new System.Drawing.Point(-1, 4);
            this.lbPrecoDe.Name = "lbPrecoDe";
            this.lbPrecoDe.Size = new System.Drawing.Size(73, 13);
            this.lbPrecoDe.TabIndex = 5;
            this.lbPrecoDe.Text = "0";
            this.lbPrecoDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbValorUnit
            // 
            this.lbValorUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbValorUnit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValorUnit.Location = new System.Drawing.Point(0, 0);
            this.lbValorUnit.Name = "lbValorUnit";
            this.lbValorUnit.Size = new System.Drawing.Size(67, 50);
            this.lbValorUnit.TabIndex = 4;
            this.lbValorUnit.Text = "label1";
            this.lbValorUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbValorUnit.Visible = false;
            // 
            // lbProduto
            // 
            this.lbProduto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProduto.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProduto.Location = new System.Drawing.Point(0, 0);
            this.lbProduto.Name = "lbProduto";
            this.lbProduto.Size = new System.Drawing.Size(200, 50);
            this.lbProduto.TabIndex = 1;
            this.lbProduto.Text = "label1";
            this.lbProduto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.lbQuantidade, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRemove, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(68, 50);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lbQuantidade
            // 
            this.lbQuantidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbQuantidade.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuantidade.Location = new System.Drawing.Point(25, 0);
            this.lbQuantidade.Name = "lbQuantidade";
            this.lbQuantidade.Size = new System.Drawing.Size(16, 50);
            this.lbQuantidade.TabIndex = 2;
            this.lbQuantidade.Text = "1";
            this.lbQuantidade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRemove
            // 
            this.btnRemove.BackgroundImage = global::Zip.Pdv.Properties.Resources._326491_circle_remove_icon;
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Location = new System.Drawing.Point(3, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(16, 44);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::Zip.Pdv.Properties.Resources._326501_add_circle_icon;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(47, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(18, 44);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnTaskItem
            // 
            this.btnTaskItem.BackgroundImage = global::Zip.Pdv.Properties.Resources.Trash_16;
            this.btnTaskItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTaskItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnTaskItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTaskItem.ImageLocation = "";
            this.btnTaskItem.Location = new System.Drawing.Point(0, 0);
            this.btnTaskItem.Name = "btnTaskItem";
            this.btnTaskItem.Size = new System.Drawing.Size(48, 50);
            this.btnTaskItem.TabIndex = 0;
            this.btnTaskItem.TabStop = false;
            // 
            // CupomItemTotem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panel6);
            this.Name = "CupomItemTotem";
            this.Size = new System.Drawing.Size(480, 57);
            this.Load += new System.EventHandler(this.CupomItem_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panelPrincipal.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnTaskItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbValorTotal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox btnTaskItem;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbDesconto;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbPrecoDe;
        private System.Windows.Forms.Label lbValorUnit;
        private System.Windows.Forms.Label lbProduto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbQuantidade;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
    }
}
