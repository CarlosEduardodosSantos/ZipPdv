using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;
using Zip.Utils;

namespace Zip.Pdv.Cadastro.Produto
{
    public partial class CadastroProduto : CadastroBase
    {
        private ProdutoViewModel _produtoView;
        public CadastroProduto()
        {
            InitializeComponent();

            cbSituacao.DataSource = Conversoes.Listar(typeof(SituacaoCadastroEnuView));
            cbSituacao.DisplayMember = "Value";
            cbSituacao.ValueMember = "Key";
            cbSituacao.SelectedValue = (int)SituacaoCadastroEnuView.Ativo;

            using (var unidadeAppService = Program.Container.GetInstance<IUnidadeMedidaAppService>())
            {
                var unidades = unidadeAppService.ObterTodos().ToList();
                cbUnidade.DataSource = unidades;
                cbUnidade.DisplayMember = "Descricao";
                cbUnidade.ValueMember = "Unidade";
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
            using (var deptoAppService = Program.Container.GetInstance<ProdutoDeptoAppService>())
            {
                var deptos = deptoAppService.ObterTodos().ToList();
                cbDepto.DataSource = deptos;
                cbDepto.DisplayMember = "Descricao";
                cbDepto.ValueMember = "DeptoId";
                cbDepto.SelectedValue = -1;
            }
            using (var secaoAppService = Program.Container.GetInstance<IProdutoSecaoAppService>())
            {
                var secoes = secaoAppService.ObterTodos().ToList();
                cbSecao.DataSource = secoes;
                cbSecao.DisplayMember = "Descricao";
                cbSecao.ValueMember = "SecaoId";
                cbSecao.SelectedValue = -1;
            }
            using (var tipoAppService = Program.Container.GetInstance<IProdutoTipoAppService>())
            {
                var tipos = tipoAppService.ObterTodos().ToList();
                cbTipo.DataSource = tipos;
                cbTipo.DisplayMember = "Descricao";
                cbTipo.ValueMember = "Tipo";
                cbTipo.SelectedValue = -1;
            }
            using (var fornecedorAppService = Program.Container.GetInstance<IFornecedorAppService>())
            {
                var fornecedores = fornecedorAppService.ObterTodos().ToList();
                cbFornecedor.DataSource = fornecedores;
                cbFornecedor.DisplayMember = "Nome";
                cbFornecedor.ValueMember = "FornecedorId";
                cbFornecedor.SelectedValue = -1;
            }
            using (var tributacaoAppService = Program.Container.GetInstance<ITributacaoFiscalAppService>())
            {
                var fornecedores = tributacaoAppService.ObterTodos().ToList();
                cbTributacao.DataSource = fornecedores;
                cbTributacao.DisplayMember = "Descricao";
                cbTributacao.ValueMember = "TributacaoId";
                cbTributacao.SelectedValue = -1;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
            var item = (GalleryItem)sender;
            flowLayoutPanel1.Controls.Remove(item);
            flowLayoutPanel1.Refresh();
        }

        public override object ObjetoToClass()
        {
            _produtoView.ProdutoId = Convert.ToInt32(txtProdutoId.Text);
            _produtoView.Descricao = txtNome.Text;
            _produtoView.ProdutoTipo = Convert.ToString(cbTipo.SelectedValue);
            _produtoView.ValorVenda = txtValorVenda.ValueNumeric;
            _produtoView.ValorCusto = txtValorCusto.ValueNumeric;
            _produtoView.ValorPromocao = txtValorPromocao.ValueNumeric;
            _produtoView.ValorFidelidade = txtValorFidelidade.ValueNumeric;
            _produtoView.Unidade = Convert.ToString(cbUnidade.SelectedValue);
            _produtoView.GrupoId = Convert.ToInt32(cbGrupo.SelectedValue);
            _produtoView.DeptoId = Convert.ToInt32(cbDepto.SelectedValue);
            _produtoView.SecaoId = Convert.ToInt32(cbSecao.SelectedValue);
            _produtoView.Situacao = (SituacaoCadastroEnuView)Convert.ToInt32(cbSituacao.SelectedValue);
            _produtoView.UsaBalanca = chkBalanca.Checked;
            _produtoView.EstoqueMin = txtQtdeMin.ValueNumeric;
            _produtoView.EstoqueMax = txtQtdeMax.ValueNumeric;
            _produtoView.FornecedorId = Convert.ToInt32(cbFornecedor.SelectedValue);
            _produtoView.TributacaoId = (Guid?)cbTributacao.SelectedValue ?? Guid.Empty;

            return _produtoView;
        }

        public override void ClassToObjeto(object objeto)
        {
            _produtoView = (ProdutoViewModel) objeto;

            txtProdutoId.Text = _produtoView.ProdutoId.ToString();
            txtNome.Text = _produtoView.Descricao;
            txtValorVenda.ValueNumeric = _produtoView.ValorVenda;
            txtValorCusto.ValueNumeric = _produtoView.ValorCusto;
            txtValorPromocao.ValueNumeric = _produtoView.ValorPromocao;
            txtValorFidelidade.ValueNumeric = _produtoView.ValorFidelidade;

            cbGrupo.SelectedValue = _produtoView.GrupoId;
            cbDepto.SelectedValue = _produtoView.DeptoId;
            cbSecao.SelectedValue = _produtoView.SecaoId;
            cbSituacao.SelectedValue = (int)_produtoView.Situacao;
            cbFornecedor.SelectedValue = _produtoView.FornecedorId;
            cbTributacao.SelectedValue = _produtoView.TributacaoId;

            chkBalanca.Checked = _produtoView.UsaBalanca;
            chkIsPos.Checked = _produtoView.IsPos;

            txtEstoque.ValueNumeric = _produtoView.Estoque;
            txtQtdeMin.ValueNumeric = _produtoView.EstoqueMin;
            txtQtdeMax.ValueNumeric = _produtoView.EstoqueMax;
            txtDtUltVenda.Text = _produtoView.DataUltVenda.ToShortDateString();



            var unidades = (List<UnidadeMedidaViewModel>)cbUnidade.DataSource;
            var unidade = unidades.FirstOrDefault(t => t.Unidade == _produtoView.Unidade);
            cbUnidade.SelectedItem = unidade;

            var tipos = (List<ProdutoTipoViewModel>)cbTipo.DataSource;
            var tipo = tipos.FirstOrDefault(t => t.Tipo == _produtoView.ProdutoTipo);
            cbTipo.SelectedItem = tipo;
        }

        public override void TravaDestrava(bool operacao)
        {
            txtProdutoId.ReadOnly = operacao;
            txtCodigoFab.ReadOnly = operacao;
            txtNome.ReadOnly = operacao;
            txtValorVenda.ReadOnly = operacao;
            txtValorCusto.ReadOnly = operacao;
            txtValorPromocao.ReadOnly = operacao;
            txtValorFidelidade.ReadOnly = operacao;

            cbGrupo.Enabled = !operacao;
            cbDepto.Enabled = !operacao;
            cbSecao.Enabled = !operacao;
            cbSituacao.Enabled = !operacao;

            chkBalanca.Enabled = !operacao;
            chkIsPos.Enabled = !operacao;
            chkAplicaServico.Enabled = !operacao;

            txtEstoque.ReadOnly = operacao;
            txtQtdeMin.ReadOnly = operacao;
            txtQtdeMax.ReadOnly = operacao;
            txtDtUltVenda.ReadOnly = operacao;
            cbFornecedor.Enabled = !operacao;
            cbTributacao.Enabled = !operacao;
            cbUnidade.Enabled = !operacao;
            cbTipo.Enabled = !operacao;
            cbFabricante.Enabled = !operacao;


        }

        public override void LimparTudo()
        {
            txtProdutoId.Clear();
            txtCodigoFab.Clear();
            txtNome.Clear();
            txtValorVenda.Clear();
            txtValorCusto.Clear();
            txtValorPromocao.Clear();
            txtValorFidelidade.Clear();

            cbGrupo.SelectedIndex = -1;
            cbDepto.SelectedIndex = -1;
            cbSecao.SelectedIndex = -1;
            cbSituacao.SelectedIndex = -1;
            cbUnidade.SelectedIndex = -1;

            chkBalanca.Checked = false;
            chkIsPos.Checked = true;

            txtEstoque.ValueNumeric = 0;
            txtQtdeMin.ValueNumeric = 0;
            txtQtdeMax.ValueNumeric = 0;
            txtDtUltVenda.Clear();
            cbFornecedor.SelectedIndex = -1;
            cbTributacao.SelectedIndex = -1;
            cbTipo.SelectedIndex = -1;
            cbFabricante.SelectedIndex = -1;
        }

        public override bool ValidaCadastro()
        {
            if (string.IsNullOrEmpty(txtProdutoId.Text))
                AdicionaErroValidacao("Código do produto não informado.");

            if (string.IsNullOrEmpty(txtNome.Text))
                AdicionaErroValidacao("Descrição do produto não informado.");

            if (txtValorCusto.ValueNumeric == 0)
                AdicionaErroValidacao("Valor de custo não informado.");

            if (txtValorVenda.ValueNumeric == 0)
                AdicionaErroValidacao("Valor de venda não informado.");

            if (cbTipo.SelectedIndex == -1)
                AdicionaErroValidacao("Tipo de produto não informádo.");

            if (cbUnidade.SelectedIndex == -1)
                AdicionaErroValidacao("unidade medida não informádo.");

            if (cbTributacao.SelectedIndex == -1)
                AdicionaErroValidacao("Tributação fiscal não informádo.");

            return ResultValid.IsValid;
        }
    }
}
