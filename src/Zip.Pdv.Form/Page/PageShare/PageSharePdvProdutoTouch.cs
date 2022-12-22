using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv.Page.PageShare
{
    public partial class PageSharePdvProdutoTouch : PageBase
    {
        private List<ProdutoGrupoViewModel> _grupos;
        private List<ProdutoViewModel> _produtos;

        private int _pageQuantidade;
        private int _currentPage = 1;

        private int _pageProdQuantidade;
        private int _currentProdPage = 1;

        public PageSharePdvProdutoTouch()
        {
            InitializeComponent();
        }

        public override void Inicia()
        {
            CarregaGrupos();
            _produtos = new List<ProdutoViewModel>();
            flayoutProduto.Controls.Clear();
            CarregaMaisVendidos();
        }

        private void CarregaMaisVendidos()
        {
            using (var appServer = Program.Container.GetInstance<IProdutoAppService>())
            {
                _produtos = appServer.ObterMaisVendidos().ToList();
            }

            ProdutoPaginacao(1);
        }


        private void CarregaGrupos()
        {


            using (var appServer = Program.Container.GetInstance<IProdutoGrupoAppService>())
            {
                _grupos = appServer.ObterTodos(Program.Loja).Where(t=> t.IsPos).ToList();
            }

            GrupoPaginacao(1);

        }

        private void GrupoPaginacao(int page)
        {
            if (flayoutGrupo.Width == 0) return;
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;

            int itens = (flayoutGrupo.Width) / 155 * 2;

            var skip = itens * (page - 1);

            _pageQuantidade = int.Parse(Math.Ceiling(_grupos.Count() / double.Parse(itens.ToString())).ToString());
            if (_pageQuantidade > 1)
                btnNext.Enabled = true;

            var gruposPadding = _grupos.Skip(skip).Take(itens).ToList();

            if (_grupos.Count > 0)
            {
                flayoutGrupo.Controls.Clear();
                foreach (var t in gruposPadding)
                {
                    var btnGrupo = new GrupoGridViewItem();
                    btnGrupo.BackColor = ColorHelper.ObterColor();
                    btnGrupo.ColorText = ColorHelper.ObterColorFonte(btnGrupo.BackColor);
                    btnGrupo.AdiconaDataSource(t);
                    btnGrupo.SelectItem += BtnGrupo_Click;

                    flayoutGrupo.Controls.Add(btnGrupo);
                }
            }
            else
                TouchMessageBox.Show("Grupo do PDV não cadastrado.", "E-Ticket", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void BtnGrupo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < flayoutGrupo.Controls.Count; i++)
            {
                if (flayoutGrupo.Controls[i].GetType() != typeof(GrupoGridViewItem))
                    continue;

                ((GrupoGridViewItem)flayoutGrupo.Controls[i]).BorderStyle = BorderStyle.None;

            }

            var btn = (GrupoGridViewItem)sender;
            btn.BorderStyle = BorderStyle.Fixed3D;

            //btn.Theme = Theme.MSOffice2010_Green;

            flayoutGrupo.Refresh();

            CarregaProdutos(btn.CodigoProduto);
        }

        private void CarregaProdutos(int grupoId)
        {
            using (var appServer = Program.Container.GetInstance<IProdutoAppService>())
            {
                _produtos = appServer.ObterPorGrupoId(int.Parse(grupoId.ToString()), Program.Loja).ToList();
            }

            ProdutoPaginacao(1);
        }

        private void ProdutoPaginacao(int page)
        {
            if (flayoutProduto.Width == 0) return;
            btnNextProd.Enabled = false;
            btnPrevProd.Enabled = false;

            int qItemW = flayoutProduto.Width / 95;
            int qItemH = flayoutProduto.Height / 53;
            int itens = qItemW * qItemH;

            var skip = itens * (page - 1);

            _pageProdQuantidade = int.Parse(Math.Ceiling(_produtos.Count() / double.Parse(itens.ToString())).ToString());
            if (_pageProdQuantidade > 1)
                btnNextProd.Enabled = true;

            flayoutProduto.Controls.Clear();

            var produtoPadding = _produtos.Skip(skip).Take(itens).ToList();
            if (produtoPadding.Count > 0)
            {
                var controls = new Control[produtoPadding.Count];
                var index = 0;
                foreach (var t in produtoPadding)
                {
                    var btnGridItem = new ProdutoGridViewItemImage()
                    {
                        Index = index
                    };
                    btnGridItem.SelectItem += selectedItem;
                    btnGridItem.AdiconaDataSource(t);

                    controls[index] = btnGridItem;

                    index++;

                }
                flayoutProduto.Controls.AddRange(controls);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            GrupoPaginacao(_currentPage);
            if (_currentPage == _pageQuantidade)
                btnNext.Enabled = false;
            if (_currentPage > 1)
                btnPrevious.Enabled = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            GrupoPaginacao(_currentPage);

            if (_currentPage == 1)
                btnPrevious.Enabled = false;
        }

        private void btnNextProd_Click(object sender, EventArgs e)
        {
            _currentProdPage++;
            ProdutoPaginacao(_currentProdPage);
            if (_currentProdPage == _pageProdQuantidade)
                btnNextProd.Enabled = false;
            if (_currentProdPage > 1)
                btnPrevProd.Enabled = true;
        }

        private void btnPrevProd_Click(object sender, EventArgs e)
        {
            _currentProdPage--;
            ProdutoPaginacao(_currentProdPage);

            if (_currentProdPage == 1)
                btnPrevProd.Enabled = false;
        }
    }
}
