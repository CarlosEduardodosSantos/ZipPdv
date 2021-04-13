using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component.EspeciePagamento;
using Zip.Pdv.Helpers;

namespace Zip.Pdv
{
    public partial class FormObservacao : Form
    {
        private List<ProdutoObservacaoViewModel> _observacoes;
        public VendaItemViewModel VendaItem;
        private UcItemObservacao _itemObservacao;
        public FormObservacao(VendaItemViewModel vendaItem)
        {

            VendaItem = vendaItem;

            InitializeComponent();

            lbProdutoNome.Text = vendaItem.Produto;

        }

        private void FormComplementos_Load(object sender, System.EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            using (var produtoAppService = Program.Container.GetInstance<IProdutoAppService>())
            {
                _observacoes = produtoAppService.ObterProdutoObservacao(VendaItem.ProdutoViewModel.GrupoId).ToList();
                foreach (var produtoObservacaoViewModel in _observacoes)
                {
                    var item = new Button();
                    item.Width = 180;
                    item.Height = 50;
                    item.BackColor = Color.Orange;
                    item.Text = produtoObservacaoViewModel.Descricao;
                    item.Click += Item_SelectItem;
                    flowLayoutPanel1.Controls.Add(item);
                    
                }

                CarregaDescricao();
            }

        }

       
        private void Item_SelectItem(object sender, System.EventArgs e)
        {
            var obj = (Button)sender;


            VendaItem.Observacao += $" - {obj.Text}";
            CarregaDescricao();
        }

        private void CarregaDescricao()
        {
            flowLayoutPanel2.Controls.Clear();

            _itemObservacao = new UcItemObservacao();
            _itemObservacao.Width = flowLayoutPanel2.Width - 10;
            _itemObservacao.Height = flowLayoutPanel2.Height - 10;

            _itemObservacao.AdicionarObswervacao(VendaItem.Observacao);
            _itemObservacao.Click += Item_Click;
            flowLayoutPanel2.Controls.Add(_itemObservacao);
            _itemObservacao.SetFocus();

        }

        private void Item_Click(object sender, EventArgs e)
        {

            VendaItem.Observacao = string.Empty;
            CarregaDescricao();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            TecladoVirtualHelper.Close();

            VendaItem.Observacao = _itemObservacao.Observacao;
            Close();
        }
    }
}
