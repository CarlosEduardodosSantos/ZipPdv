using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.CupomGrid
{
    public partial class CupomItemTotem : CupomItemBase
    {

        public CupomItemTotem()
        {
            InitializeComponent();
            btnTaskItem.Click += taskItem;
            btnAdd.Click += addItem;
            btnRemove.Click += removeItem;

           

        }

        private void CupomItem_Load(object sender, System.EventArgs e)
        {
            //CarregaItem();
        }

        public override void CarregaItem(bool disableExcluir = false)
        {
            if (DataSource == null) return;

            btnTaskItem.Visible = !disableExcluir;
            btnAdd.Visible = !disableExcluir;
            btnRemove.Visible = !disableExcluir;
            //btnRemove.Enabled = DataSource.Quantidade > 1;
            if (DataSource.VendaComplementos.Any())
            {
                foreach (var item in DataSource.VendaComplementos)
                {
                    this.Height += 20;
                    //panelPrincipal.Dock = DockStyle.Top;

                    var label = new Label();
                    label.Text = item.Descricao;
                    label.Font = Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.AutoSize = false;
                    label.Height = 20;
                    label.Dock = DockStyle.Bottom;
                    label.Padding = new Padding(10, 0, 0, 0);
                    panelPrincipal.Controls.Add(label);
                }

            }
            if (DataSource.VendaProdutoOpcoes.Any())
            {
                foreach (var item in DataSource.VendaProdutoOpcoes)
                {
                    this.Height += 20;
                    //panelPrincipal.Dock = DockStyle.Top;

                    var label = new Label();
                    label.Text = item.Descricao;
                    label.Font = Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.AutoSize = false;
                    label.Height = 20;
                    label.Dock = DockStyle.Bottom;
                    label.Padding = new Padding(10, 0, 0, 0);
                    panelPrincipal.Controls.Add(label);
                }

            }
            if (!string.IsNullOrEmpty(DataSource.Observacao))
            {
                this.Height += 20;
                //panelPrincipal.Dock = DockStyle.Top;

                var label = new Label();
                label.Text = DataSource.Observacao;
                label.Font = Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label.AutoSize = false;
                label.Height = 20;
                label.Dock = DockStyle.Bottom;
                label.Padding = new Padding(10, 0, 0, 0);
                panelPrincipal.Controls.Add(label);
            }
            lbProduto.Text = DataSource.Produto;
            lbValorUnit.Text = (DataSource.ValorUnitatio - DataSource.Desconto).ToString("N2");
            lbQuantidade.Text = DataSource.Quantidade.ToString("N0");
            lbValorTotal.Text = $"{DataSource.ValorTotal.ToString("C2")}";
        
            if (DataSource.Desconto > 0)
            {
                lbDesconto.Text = $"(-{DataSource.Desconto.ToString("N2")})";
            }
            else
            {
                lbDesconto.Visible = false;
            }
            if (DataSource.ValorDe > 0)
            {
                lbPrecoDe.Text = $"De {DataSource.ValorDe.ToString("N2")}";
            }
            else
            {
                lbPrecoDe.Visible = false;
            }
           /* if (!string.IsNullOrEmpty(DataSource.ProdutoViewModel?.Imagem))
            {
                imageProduto.Visible = true;
                imageProduto.Image = Funcoes.Base64ToImage(DataSource.ProdutoViewModel.Imagem);
                imageProduto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                imageProduto.Visible = false;
            }*/
        }

        public override void Refresh()
        {
            CarregaItem();
            base.Refresh();
        }
    }
}
