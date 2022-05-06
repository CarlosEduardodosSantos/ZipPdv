using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Utils;

namespace Zip.Pdv.Page
{
    public partial class PageProdutoCadastro : PageBase
    {
        public PageProdutoCadastro()
        {
            InitializeComponent();
            btnVoltar.Click += closeForm;

            cbSituacao.DataSource = Conversoes.Listar(typeof(SituacaoCadastroEnuView));
            cbSituacao.DisplayMember = "Value";
            cbSituacao.ValueMember = "Key";
            cbSituacao.SelectedValue = (int)SituacaoCadastroEnuView.Ativo;

            using (var unidadeAppService = Program.Container.GetInstance<IUnidadeMedidaAppService>())
            {
                var unidades = unidadeAppService.ObterTodos().ToList();
                cbUnidade.DataSource = unidades;
                cbUnidade.DisplayMember = "Descricao";
                cbUnidade.ValueMember = "UnidadeId";
                cbUnidade.SelectedValue = -1;
            }

            using (var grupoAppService = Program.Container.GetInstance<IProdutoGrupoAppService>())
            {
                var grupos = grupoAppService.ObterTodos().ToList();
                cbGrupo.DataSource = grupos;
                cbGrupo.DisplayMember = "Descricao";
                cbGrupo.ValueMember = "GrupoId";
                cbGrupo.SelectedValue = -1;
            }
            using (var tipoAppService = Program.Container.GetInstance<IProdutoTipoAppService>())
            {
                var tipos = tipoAppService.ObterTodos().ToList();
                cbTipo.DataSource = tipos;
                cbTipo.DisplayMember = "Descricao";
                cbTipo.ValueMember = "ProdutoTipoId";
                cbTipo.SelectedValue = -1;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnAdicionarImagem_Click(object sender, System.EventArgs e)
        {
            var open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {

                
                var image = new Bitmap(open.FileName);

                var ctrl = new GalleryItem();
                ctrl.imagem = image;
                ctrl.TaskItem += ImageViewOnTaskItem;


                flowLayoutPanel1.Controls.Add(ctrl);
            }

        }

        private void ImageViewOnTaskItem(object sender, EventArgs eventArgs)
        {
            var item = (GalleryItem) sender;
            flowLayoutPanel1.Controls.Remove(item);
            flowLayoutPanel1.Refresh();
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {

        }
    }
}
