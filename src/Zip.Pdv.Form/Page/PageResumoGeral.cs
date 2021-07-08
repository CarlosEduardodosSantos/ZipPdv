using System;
using System.Windows.Forms;
using Zip.Pdv.Page.PageShare;

namespace Zip.Pdv.Page
{
    public partial class PageResumoGeral : PageBase
    {
        private PageBase _pageBase;
        public PageResumoGeral()
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;
        }

        private void btnResumoVenda_Click(object sender, EventArgs e)
        {
            _pageBase = new PageShareResumoVenda();
            OpenPageShare(_pageBase);
        }

        private void OpenPageShare(PageBase page)
        {
            panelResumo.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelResumo.Controls.Add(page);
        }

        private void PageResumoGeral_Load(object sender, EventArgs e)
        {
            btnResumoVenda.PerformClick();
        }

        private void btnResumoProdutos_Click(object sender, EventArgs e)
        {
            _pageBase = new PageShareProduto();
            OpenPageShare(_pageBase);
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            _pageBase.Atualizar();
        }
    }
}
