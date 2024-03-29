﻿using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zip.Pdv.Component;
using Zip.Pdv.Component.EspeciePagamento;

namespace Zip.Pdv
{
    public partial class FrmProdutoOpcaoToten : Form
    {
        public VendaItemViewModel VendaItem;
        private readonly List<ProdutoOpcaoTipoVewModel> _produtosTipoOpoes;
        private ProdutoOpcaoTipoVewModel _tipo;
        private string[] _tabs;
        private int _tabindex;
        public FrmProdutoOpcaoToten(List<ProdutoOpcaoTipoVewModel> produtosTipoOpoes, VendaItemViewModel vendaItem)
        {
            InitializeComponent();
            _produtosTipoOpoes = produtosTipoOpoes;
            VendaItem = vendaItem;
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            tablessControl1.Selected += tabControl1_Selected;
            lbProdutoNome.Text = VendaItem.DescricaoProduto;
            bunifuFlatButton2.Enabled = false;

            CarregaOpcoes();
        }

        void CarregaOpcoes()
        {
            _tabs = new string[_produtosTipoOpoes.Count];

            for (int i = 0; i < _produtosTipoOpoes.Count; i++)
            {
                var item = _produtosTipoOpoes[i];
                _tabs[i] = $"tab{item.ProdutosOpcaoTipoId}";

                var table = new TabPage(item.Nome);
                table.Name = _tabs[i];

                // table.Text = item.Nome;

                tablessControl1.TabPages.Add(table);

                var ucOpcoes = new ProdutoListView(item);
                ucOpcoes.Dock = DockStyle.Fill;
                ucOpcoes.SelectItem += UcOpcoes_SelectItem;
                table.Controls.Add(ucOpcoes);
            }

            tablessControl1.SelectedIndex = _tabindex;
            tabControl1_Selected(tablessControl1, null);
        }

        private void UcOpcoes_SelectItem(object sender, EventArgs e)
        {
            var item = (ProdutoGridViewItem)sender;
            var produtoOpcao = (ProdutoOpcaoViewModel)item.SelectedItem;
            var tipo = _produtosTipoOpoes.FirstOrDefault(t => t.ProdutosOpcaoTipoId == produtoOpcao.ProdutosOpcaoTipoId);

            if (tipo.QtdeMax > 0)
            {
                var qtdeLancTipo = VendaItem.VendaProdutoOpcoes.Where(t => t.ProdutosOpcaoTipoId == produtoOpcao.ProdutosOpcaoTipoId).Count();
                if (qtdeLancTipo >= tipo.QtdeMax)
                {
                    TouchMessageBox.Show($"Não é permitido escolher mais que {tipo.QtdeMax}\npara essa opção.", "Validação", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    return;
                }
            }

            VendaItem.VendaProdutoOpcoes.Add(new VendaProdutoOpcaoViewModel()
            {
                ProdutosOpcaoId = produtoOpcao.ProdutosOpcaoId,
                ProdutosOpcaoTipoId = produtoOpcao.ProdutosOpcaoTipoId,
                ProdutoId = VendaItem.ProdutoId,
                Sequencia = VendaItem.SeqProduto,
                Valor = produtoOpcao.Valor,
                Descricao = produtoOpcao.Nome
            });
            CarregaDescricao();

        }

        private void CarregaDescricao()
        {
            flowLayoutPanel2.Controls.Clear();
            foreach (var vendaItemVendaProdutoOpcoes in VendaItem.VendaProdutoOpcoes)
            {
                var item = new UcPdvItem();
                item.AdicionarProdutoOpcoes(vendaItemVendaProdutoOpcoes);
                item.Click += Item_Click;
                flowLayoutPanel2.Controls.Add(item);
                //btn.Anchor = AnchorStyles.None;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = (UcPdvItem)sender;
            var source = (VendaProdutoOpcaoViewModel)item.CaixaSource;

            var tipo = _produtosTipoOpoes.FirstOrDefault(t => t.ProdutosOpcaoTipoId == source.ProdutosOpcaoTipoId);
            if (lbTabNome.Text != tipo.Nome)
            {
                TouchMessageBox.Show($"Esse complemento não faz parte desse complementar atual.", "Validação", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            VendaItem.VendaProdutoOpcoes.Remove(source);
            CarregaDescricao();
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            var tab = tablessControl1.TabPages[_tabindex];
            lbTabNome.Text = tab.Text;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (!bunifuFlatButton1.Enabled) return;

            _tipo = _produtosTipoOpoes[_tabindex];
            if (_tipo.Obrigatorio)
            {
                var qtdeLancTipo = VendaItem.VendaProdutoOpcoes.Where(t => t.ProdutosOpcaoTipoId == _tipo.ProdutosOpcaoTipoId).Count();
                if (qtdeLancTipo == 0)
                {
                    TouchMessageBox.Show($"Item com seleção obrigatória.", "Validação", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    return;
                }
            }

            _tabindex++;
            tablessControl1.SelectedIndex = _tabindex;

            if (_tabindex + 1 >= _tabs.Count())
            {
                bunifuFlatButton1.Enabled = false;
                bunifuFlatButton3.Enabled = true;
            }
            bunifuFlatButton2.Enabled = _tabindex > 0;

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (!bunifuFlatButton2.Enabled) return;

            _tabindex--;
            tablessControl1.SelectedIndex = _tabindex;


            bunifuFlatButton2.Enabled = _tabindex > 0;
            bunifuFlatButton1.Enabled = _tabindex + 1 < _tabs.Count();
            bunifuFlatButton3.Enabled = _tabindex + 1 >= _tabs.Count();

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (!bunifuFlatButton3.Enabled) return;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

}
