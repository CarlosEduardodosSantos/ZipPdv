
namespace Zip.Pdv.Component
{
    partial class ProdutoListView
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
            this.flayoutProduto = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNextProd = new System.Windows.Forms.Button();
            this.btnPrevProd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flayoutProduto
            // 
            this.flayoutProduto.BackColor = System.Drawing.Color.White;
            this.flayoutProduto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flayoutProduto.Location = new System.Drawing.Point(68, 0);
            this.flayoutProduto.Name = "flayoutProduto";
            this.flayoutProduto.Size = new System.Drawing.Size(760, 472);
            this.flayoutProduto.TabIndex = 12;
            // 
            // btnNextProd
            // 
            this.btnNextProd.BackColor = System.Drawing.Color.Orange;
            this.btnNextProd.BackgroundImage = global::Zip.Pdv.Properties.Resources.next_resultset_icone_3882_32;
            this.btnNextProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNextProd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextProd.Enabled = false;
            this.btnNextProd.FlatAppearance.BorderSize = 0;
            this.btnNextProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextProd.Location = new System.Drawing.Point(828, 0);
            this.btnNextProd.Name = "btnNextProd";
            this.btnNextProd.Size = new System.Drawing.Size(57, 472);
            this.btnNextProd.TabIndex = 11;
            this.btnNextProd.UseVisualStyleBackColor = false;
            // 
            // btnPrevProd
            // 
            this.btnPrevProd.BackColor = System.Drawing.Color.Orange;
            this.btnPrevProd.BackgroundImage = global::Zip.Pdv.Properties.Resources.previous_resultset_icone_6623_32;
            this.btnPrevProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrevProd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevProd.Enabled = false;
            this.btnPrevProd.FlatAppearance.BorderSize = 0;
            this.btnPrevProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevProd.Location = new System.Drawing.Point(0, 0);
            this.btnPrevProd.Name = "btnPrevProd";
            this.btnPrevProd.Size = new System.Drawing.Size(68, 472);
            this.btnPrevProd.TabIndex = 10;
            this.btnPrevProd.UseVisualStyleBackColor = false;
            // 
            // ProdutoListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flayoutProduto);
            this.Controls.Add(this.btnNextProd);
            this.Controls.Add(this.btnPrevProd);
            this.Name = "ProdutoListView";
            this.Size = new System.Drawing.Size(885, 472);
            this.Load += new System.EventHandler(this.ProdutoListView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flayoutProduto;
        private System.Windows.Forms.Button btnNextProd;
        private System.Windows.Forms.Button btnPrevProd;
    }
}
