using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FormComplementos : Form
    {
        private List<ProdutoComplementoViewModel> _complementos;
        public VendaItemViewModel VendaItem;
        public FormComplementos(List<ProdutoComplementoViewModel> complementos, VendaItemViewModel vendaItem)
        {
            _complementos = complementos;
            VendaItem = vendaItem;
            
            InitializeComponent();

            lbProdutoNome.Text = vendaItem.Produto;

        }

        private void FormComplementos_Load(object sender, System.EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (var produtoComplementoViewModel in _complementos)
            {
                var item = new ProdutoGridViewItem();
                item.BackColor = Color.Orange;
                item.AdiconaComplemento(produtoComplementoViewModel);
                item.SelectItem += Item_SelectItem;
                flowLayoutPanel1.Controls.Add(item);
            }

            CarregaDescricao();
        }
        
        private void Item_SelectItem(object sender, System.EventArgs e)
        {
            var obj = (ProdutoGridViewItem) sender;
            var complemento = (ProdutoComplementoViewModel)obj.SelectedItem;

            VendaItem.VendaComplementos.Add(new VendaComplementoViewModel()
            {
                ComplementoId = complemento.ComplementoId,
                ProdutoId = VendaItem.ProdutoId,
                Sequencia = VendaItem.SeqProduto,
                Valor = complemento.Valor,
                Descricao = complemento.Descricao
            });
            CarregaDescricao();
        }

        private void CarregaDescricao()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (var vendaItemVendaComplemento in VendaItem.VendaComplementos)
            {
                var item = new UcPdvItem();
                item.AdicionarComplemento(vendaItemVendaComplemento);
                item.Click += Item_Click;
                flowLayoutPanel2.Controls.Add(item);
                //btn.Anchor = AnchorStyles.None;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem) sender;
            var source = (VendaComplementoViewModel)item.CaixaSource;
            VendaItem.VendaComplementos.Remove(source);
            CarregaDescricao();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
