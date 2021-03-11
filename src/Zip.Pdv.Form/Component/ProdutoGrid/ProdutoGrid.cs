using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.ProdutoGrid
{
    public partial class ProdutoGrid : UserControl
    {
        public event EventHandler<EventArgs> SelectItem;
        public event EventHandler<PreviewKeyDownEventArgs> KeyDownItem;
        void selectItem(object sender, EventArgs e)
        {
            var completedEvent = SelectItem;
            if (completedEvent != null)
            {
                var item = (ProdutoGridItem)sender;
                completedEvent(item, e);
            }
        }
        void keyDownItem(object sender, PreviewKeyDownEventArgs e)
        {

            var completedEvent = KeyDownItem;
            if (completedEvent != null)
            {
    
                var item = (ProdutoGridItem)sender;
                completedEvent(item, e);
            }
        }
        public ProdutoGrid()
        {
            InitializeComponent();
            fLayoutProdutos.AutoScroll = true;
        }
        public void IniciarGrid(List<ProdutoViewModel> produtoViews)
        {
            fLayoutProdutos.Controls.Clear();
            var width = fLayoutProdutos.Width - 25;

            foreach (var produtoViewModel in produtoViews)
            {
                var itemGrid = new ProdutoGridItem(produtoViewModel);
                
                itemGrid.Dock = DockStyle.Top;
                itemGrid.Width = width;
                itemGrid.SelectItem += selectItem;
                itemGrid.KeyDownItem += keyDownItem;
                fLayoutProdutos.Controls.Add(itemGrid);

            }
        }
    }
}
