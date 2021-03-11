using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Toch.Component;

namespace Toch
{
    public partial class FormLancaFicha : Form
    {
        private readonly VendedorViewModel _vendedorView;

        public FormLancaFicha(FichaViewModel ficha, VendedorViewModel vendedorView)
        {
            _fichaView = ficha;
            _vendedorView = vendedorView;

            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;
            txtPesquisa.Focus();
        }

        //Ficha
        private readonly FichaViewModel _fichaView;
        private List<VendaFichaViewModel> _listVendaFichaView;
        private VendaFichaViewModel _vendaFichaView;
        //Produto
        private List<ProdutoGrupoViewModel> _listGrupos;
        private List<ProdutoViewModel> _listProdutosView;
        private List<ProdutoViewModel> _listProdutosMeioMeioView;
        private ProdutoViewModel _produtoView;
        private List<ProdutoComplementoViewModel> _listComplementoViewModels;
        //Complemento
        private VendaComplementoViewModel _vendaComplemento;
        private List<VendaComplementoViewModel> _listVendaComplementoViewModels;
        //Meio Meio
        private List<VendaMeioMeioViewModel> _listMeioViewModels;

        int _grupoProduto;

        //Botoes
        private XButton _btnGrupo;
        private XButton _btnProduto;
        private XButton _btnComplemento;
        private void FrmMesaItens_Load(object sender, EventArgs e)
        {
            _listVendaFichaView = new List<VendaFichaViewModel>();
            _listVendaComplementoViewModels = new List<VendaComplementoViewModel>();
            _listMeioViewModels = new List<VendaMeioMeioViewModel>();
            
            Refresh(sender, e);
            CarregaHistoricoFicha();

            txtPesquisa.Focus();
        }

        private void CarregaHistoricoFicha()
        {
            using (var appFichaItem = Program.Container.GetInstance<IVendaFichaAppService>())
            {
                var listFichaHistorico = appFichaItem.ObterPorFicha(_fichaView.FichaNumero).ToList();

                inclanc = listFichaHistorico.Count > 0 ? (listFichaHistorico.Max(p => (int)p.Sequencia) + 1) : 1;

                dgvItensMesa.AutoGenerateColumns = false;

                lbDescricaoMesa.Text = "FICHA: "+_fichaView.FichaNumero;
                lbDataAbertura.Text = _fichaView.ClienteFicha?.Nome;
                lbGarcom.Text = _vendedorView.Nome;
                dgvItensMesa.DataSource = listFichaHistorico;
                //lbValorMesaTotal.Text = "R$ " + listFichaHistorico.Sum(p => p.ValorTotal).ToString("N2");

            }
        }

        public void Refresh(object sender, EventArgs e)
        {
            using (var appServer = Program.Container.GetInstance<IProdutoGrupoAppService>())
            {
                _listGrupos = appServer.ObterTodos().ToList();
            }


            flpGrupoProduto.Controls.Clear();
            //Color backColor;

            if (_listGrupos.Count > 0)
            {

                foreach (ProdutoGrupoViewModel t in _listGrupos)
                {
                    var btnGrupo = new XButton();
                    btnGrupo.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    btnGrupo.ForeColor = Color.Black;
                    btnGrupo.Text = t.Descricao;
                    btnGrupo.CodigoProduto = t.GrupoId;
                    btnGrupo.Height = 53;
                    btnGrupo.Width = 95;
                    btnGrupo.Click += BtnGrupo_Click;

                    flpGrupoProduto.Controls.Add(btnGrupo);
                }
            }
            else
                TouchMessageBox.Show("Grupo do PDV não cadastrado.", "E-Ticket", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            splitContainer1.Panel2Collapsed = true;
            dataGridView1.Select();
        }

        private void BtnGrupo_Click(object sender, EventArgs e)
        {
            if (_btnGrupo != null)
                _btnGrupo.Theme = Theme.MSOffice2010_BLUE;

            flpGrupoProduto.Refresh();

            _btnGrupo = ((XButton) sender);
            _btnGrupo.Theme = Theme.MSOffice2010_Yellow;

            _grupoProduto = _btnGrupo.CodigoProduto;

            splitContainer1.Panel2Collapsed = true;
            groupProdutos.Enabled = true;
            flpProdutos.Controls.Clear();
            flpComplementos.Controls.Clear();

            
            groupProdutos.Text = "Produtos";
            if (_grupoProduto == 0) return;

            using (var appServer = Program.Container.GetInstance<IProdutoAppService>())
            {
                _listProdutosView = appServer.ObterPorGrupoId(int.Parse(_grupoProduto.ToString())).ToList();
            }

            if (_listProdutosView.Count > 0)
            {
                var index =0;
                foreach (ProdutoViewModel t in _listProdutosView)
                {
                    index++;
                    var btnProduto = new XButton
                    {
                        Name = "Produto_"+ index,
                        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                        ForeColor = Color.Black,
                        Theme = Theme.MSOffice2010_Publisher,
                        Text = t.Descricao,
                        Height = 53,
                        Width = 95,
                        CodigoProduto = t.ProdutoId,
                        ValorUnitario = t.ValorVenda,
                        TipoProduto = t.ProdutoTipo
                    };

                    btnProduto.MouseClick += new MouseEventHandler(BtnProd_Click);
                    //btnProduto.DoubleClick += new EventHandler(BtnComplemento_DoubleClick);

                    flpProdutos.Controls.Add(btnProduto);
                }
            }

        }

        private int _indexTeste = 0;
        private void BtnProd_Click(object sender, EventArgs e)
        {
            if(_btnProduto != null)
                _btnProduto.Theme = Theme.MSOffice2010_Publisher;

            flpProdutos.Refresh();

            _btnProduto = null;
            _btnProduto = (XButton)sender;
            //_btnProduto.Enabled = false;
            _btnProduto.Theme = Theme.MSOffice2010_Green;

            #region Meio Meio
            if (_btnProduto.TipoProduto == "M")
            {
                //groupGrupo.Enabled = false;
                splitContainer1.Panel2Collapsed = true;
                flpProdutos.Controls.Clear();
                flpComplementos.Controls.Clear();

                groupProdutos.Text = "Produtos Meio a Meio";

                if (_grupoProduto == 0) return;

                using (var appServer = Program.Container.GetInstance<IProdutoAppService>())
                {
                    _listProdutosMeioMeioView = appServer.ObterMeioMeio().ToList();
                }

                if (_listProdutosMeioMeioView.Count > 0)
                {
                    flpGrupoProduto.Controls.Clear();
                    foreach (ProdutoViewModel t in _listProdutosMeioMeioView)
                    {
                        var btnProdMeioMeio = new XButton()
                        {
                            Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular),
                            Theme = Theme.MSOffice2010_Publisher,
                            ForeColor = Color.Black,
                            Height = 53,
                            Width = 95
                        };

                        btnProdMeioMeio.Text = t.Descricao;
                        btnProdMeioMeio.CodigoProduto = t.ProdutoId;
                        btnProdMeioMeio.ValorUnitario = t.ValorVenda;
                        btnProdMeioMeio.TipoProduto = t.ProdutoTipo;

                        btnProdMeioMeio.Click += BtnMeioMeio_Click;

                        flpGrupoProduto.Controls.Add(btnProdMeioMeio);
                    }
                }
            }
            #endregion

            _vendaFichaView = new VendaFichaViewModel();

            _vendaFichaView.ProdutoId = _btnProduto.CodigoProduto;
            _vendaFichaView.NomeProduto += _btnProduto.Text;
            _vendaFichaView.Quantidade = 1;
            _vendaFichaView.ValorUnitatio = _btnProduto.ValorUnitario;
            _vendaFichaView.ValorTotal = (_vendaFichaView.ValorUnitatio * 1);
            _vendaFichaView.ClienteFichaId = _fichaView.ClienteFichaId;
            _vendaFichaView.VendedorId = _vendedorView.VendedorId;

            lbGarcom.Text = _indexTeste++.ToString();
            AdicionaItenGrid(_vendaFichaView);
        }
        decimal ValorProdutos;
        private void BtnMeioMeio_Click(object sender, EventArgs e)
        {

            var btnProdMeioMeio = (TextBoxProd)sender;

            DataGridViewSelectedRowCollection theRowsSelected = dataGridView1.SelectedRows;
            if (theRowsSelected.Count > 0)
            {
                if (_listProdutosMeioMeioView == null)
                    _listProdutosMeioMeioView = new List<ProdutoViewModel>();

                _vendaFichaView = _listVendaFichaView[theRowsSelected[0].Index];
                var vendaMeioMeio = new VendaMeioMeioViewModel();

                vendaMeioMeio.NomeProduto = btnProdMeioMeio.Text;
                vendaMeioMeio.ProdutoId = btnProdMeioMeio.CodigoProduto;
                vendaMeioMeio.ValorVenda = btnProdMeioMeio.ValorUnitario;
                vendaMeioMeio.Seguencia = _vendaFichaView.Sequencia;
                vendaMeioMeio.OperacaoId = int.Parse(_vendaFichaView.Ficha);
                vendaMeioMeio.OperacaoTipo = "F";
                vendaMeioMeio.Quantidade = 1;
                vendaMeioMeio.QtdeImpresso = "1/2";
                _listMeioViewModels.Add(vendaMeioMeio);

                ValorProdutos = btnProdMeioMeio.ValorUnitario;
                _vendaFichaView.Ficha = btnProdMeioMeio.CodigoProduto.ToString();
                _vendaFichaView.NomeProduto = "1/2 " + btnProdMeioMeio.Text;


                //var refresh = new TextBoxProd {CodigoProduto = _grupoProduto};

                groupProdutos.Enabled = false;
                //groupGrupo.Enabled = true;

                _vendaFichaView.ValorTotal = ValorProdutos > btnProdMeioMeio.ValorUnitario ? ValorProdutos : btnProdMeioMeio.ValorUnitario;

                AtualizaGrid();
            }
            else
            {
                var btnrefresh = new TextBoxProd { CodigoProduto = _grupoProduto };
                //groupGrupo.Enabled = true;
                BtnProd_Click(btnrefresh, new EventArgs());
            }
        }

        private void BtnComplemento_Click(object sender, EventArgs e)
        {

            var btnProdComplemento = (XButton)sender;

            _vendaComplemento = new VendaComplementoViewModel
            {
                Ficha = _vendaFichaView.Ficha,
                ProdutoId = _vendaFichaView.ProdutoId,
                ComplementoId = btnProdComplemento.CodigoProduto,
                Valor = btnProdComplemento.ValorUnitario
            };


            var theRowsSelected = dataGridView1.SelectedRows;

            _vendaFichaView = _listVendaFichaView[theRowsSelected[0].Index];
            _vendaFichaView.ProdutoId = btnProdComplemento.CodigoProduto;
            _vendaFichaView.NomeProduto += " + " + btnProdComplemento.Text;
            _vendaFichaView.Quantidade = 1;
            _vendaFichaView.ValorUnitatio += (decimal)_vendaComplemento.Valor;
            _vendaFichaView.ValorTotal = (_vendaFichaView.ValorTotal * _vendaFichaView.Quantidade);
            AtualizaGrid();

        }

        private void AtualizaGrid()
        {
            index = 0;
            var list = new List<VendaFichaViewModel>();
            foreach (VendaFichaViewModel itens in _listVendaFichaView)
            {
                list.Add(itens);
                index++;
            }
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;

            if (dataGridView1.Rows.Count > 0 && index >= 0)
                dataGridView1.Rows[index - 1].Selected = true;


            lbQtdeLanc.Text = _listVendaFichaView.Count.ToString();
            lbSubTotal.Text = _listVendaFichaView.Sum(P => P.ValorTotal).ToString("N2");
            lbTotal.Text = _listVendaFichaView.Sum(P => P.ValorTotal).ToString("N2");
        }

        private void btnGravarItens_Click(object sender, EventArgs e)
        {
            try
            {
                using (var appFichaItem = Program.Container.GetInstance<IVendaFichaAppService>())
                {
                    appFichaItem.Add(_listVendaFichaView);
                }

            }
            catch
            {
                TouchMessageBox.Show("Erro ao lançar a ficha","", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (VendaComplementoViewModel vendaComplementoView in _listVendaComplementoViewModels)
            {
                try
                {
                    vendaComplementoView.Ficha = _vendaFichaView.Ficha;
                    using (var appServiceVendaComplemento = Program.Container.GetInstance<IVendaComplementoAppService>())
                    {
                        appServiceVendaComplemento.Add(vendaComplementoView);
                    }
                }
                catch
                {
                    TouchMessageBox.Show("Erro ao lançar o Complemento.", "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            foreach (VendaMeioMeioViewModel meioMeio in _listMeioViewModels)
            {
                try
                {
                    meioMeio.OperacaoId = int.Parse(_vendaFichaView.Ficha);
                    meioMeio.OperacaoTipo = "F";

                    using (var appServiceMeioMeio = Program.Container.GetInstance<IVendaMeioMeioAppService>())
                    {
                        appServiceMeioMeio.Adicionar(meioMeio);
                    }
                }
                catch
                {
                    TouchMessageBox.Show("Erro ao lançar o Meio a Meio.", "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                using (var appFichaItem = Program.Container.GetInstance<IVendaFichaAppService>())
                {
                    appFichaItem.ImprimeFichaGr(_fichaView.FichaNumero);
                }
            }
            catch
            {
                TouchMessageBox.Show("Erro Imprimimir.", "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            inclanc = 0;
            Close();
        }

        int index;
        private void btnAdicionarQtde_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow select in dataGridView1.SelectedRows)
            {
                index = select.Index;
                _listVendaFichaView[index].Quantidade += 1;
                _listVendaFichaView[index].ValorTotal = (_listVendaFichaView[index].ValorUnitatio * _listVendaFichaView[index].Quantidade);
            }
            AtualizaGrid();
        }

        private void btnExcluirItens_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow select in dataGridView1.SelectedRows)
            {
                index = select.Index;

                if (_listVendaFichaView[index].Quantidade == 1)
                {
                    foreach (var complemento in _listVendaComplementoViewModels)
                    {
                        if (_listVendaFichaView[index].Sequencia == complemento.Sequencia)
                        {
                            _listVendaComplementoViewModels.Remove(complemento);
                            break;
                        }
                    }
                    _listVendaFichaView.RemoveAt(index);
                    index -= 1;
                }
                else
                {
                    _listVendaFichaView[index].Quantidade -= 1;
                    _listVendaFichaView[index].ValorTotal = (_listVendaFichaView[index].ValorUnitatio * _listVendaFichaView[index].Quantidade);
                }

                select.Selected = false;

            }
            /*
            if (!groupGrupo.Enabled)
            {
                var btnRefresh = new TextBoxProd();
                btnRefresh.CodigoProduto = _grupoProduto;
                groupGrupo.Enabled = true;
                BtnProd_Click(btnRefresh, new EventArgs());
            }
            */
            AtualizaGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmObservacao frm = new FrmObservacao();
            index = e.RowIndex;
            frm.DescricaoProd = _listVendaFichaView[index].NomeProduto;
            frm.ShowDialog();
            _listVendaFichaView[index].NomeProduto = frm.DescricaoProd;

            AtualizaGrid();
        }
        int inclanc;
        private void AdicionaItenGrid(VendaFichaViewModel vendaFichaView)
        {
            decimal valorPromocao = 0;

            /*
            decimal ValorMesa = prdutos.ValorProd(_itens.Codigo, ref valorPromocao);
            itens.IdopMesa1 = opMesa.IdopMesa1;
            if (valorPromocao > 0)
            {
                FrmPromocao frm = new FrmPromocao();
                frm.ValorPromocao = valorPromocao;
                frm.ValorVenda = ValorMesa;
                frm.Produto = itens.Descricao;
                frm.ShowDialog();
                itens.Unitario = frm.ValorSelecionado;
            }
           
            {
            */

            vendaFichaView.Ficha = _fichaView.FichaNumero;

            bool AddIntem = true;
            for (int i = 0; i < _listVendaFichaView.Count; i++)
            {
                if (_listVendaFichaView[i].ProdutoId == vendaFichaView.ProdutoId && _listVendaFichaView[i].NomeProduto == vendaFichaView.NomeProduto)
                {
                    _listVendaFichaView[i].Quantidade += vendaFichaView.Quantidade;
                    _listVendaFichaView[i].ValorTotal = (_listVendaFichaView[i].Quantidade * _listVendaFichaView[i].ValorUnitatio);
                    AddIntem = false;
                    break;
                }
            }
            if (AddIntem)
            {
                vendaFichaView.Sequencia = inclanc;
                if (_vendaComplemento != null)
                {
                    _vendaComplemento.Sequencia = vendaFichaView.Sequencia;
                    _listVendaComplementoViewModels.Add(_vendaComplemento);
                }

                vendaFichaView.ValorTotal = (vendaFichaView.ValorUnitatio * vendaFichaView.Quantidade);

                _listVendaFichaView.Add(vendaFichaView);
            }
            AtualizaGrid();
            inclanc++;
        }

        private void BtnComplemento_DoubleClick(object sender, EventArgs e)
        {
            if (_listVendaComplementoViewModels.Count > 0)
            {
                var btnProd = (TextBoxProd)sender;
                _vendaFichaView = new VendaFichaViewModel();
                _vendaFichaView.ProdutoId = btnProd.CodigoProduto;
                _vendaFichaView.NomeProduto += " + " + btnProd.Text;
                _vendaFichaView.Quantidade = 1;
                _vendaFichaView.ValorUnitatio = btnProd.ValorUnitario;
                _vendaFichaView.ValorTotal = (_vendaFichaView.ValorUnitatio * _vendaFichaView.Quantidade);
                _vendaFichaView.ClienteFichaId = _fichaView.ClienteFichaId;

                if (_listVendaFichaView != null)
                {
                    if (_listVendaFichaView.Exists(p => p.NomeProduto == _vendaFichaView.NomeProduto))
                        if (TouchMessageBox.Show("Produto ja existente. Deseja incluir?", "Produto ja lançado", MessageBoxButtons.OKCancel) == DialogResult.No)
                            _listVendaFichaView = new List<VendaFichaViewModel>();
                }
                AdicionaItenGrid(_vendaFichaView);

            }
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {

            //if (opMesa.Status != "F" && ListaItens.Count > 0)
            //{
            //    if (TouchMessageBox.Show("Os itens lançados não foram confirmados ainda! Confirma retorno ao mapa de mesas?", "Sair da Mesa",
            //        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //    {
            //        //opMesa = null;
            //        List = null;
            //        ListaComponente = null;
            //        _listGrupos = null;
            //        _listProdutosView = null;
            //        ItensLancados = 0;
            //        Close();
            //    }
            //}
            //else
                Close();
        }

        private void btnObservacao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow select = dataGridView1.SelectedRows[0];
                index = select.Index;
                FrmObservacao frm = new FrmObservacao();
                frm.DescricaoProd = _listVendaFichaView[index].NomeProduto;
                frm.ShowDialog();

                _listVendaFichaView[index].Observacao = frm.Observacao;
                _listVendaFichaView[index].NomeProduto = frm.DescricaoProd;

                AtualizaGrid();
            }
        }

        private void FormLancaFicha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                IniciaPesquisa();

        }

        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CarregaProduto();
        }

        private void CarregaProduto()
        {
            int produtoId;
            int.TryParse(txtPesquisa.Text, out produtoId);
            if (produtoId > 0)
            {
                using (var appServiceProduto = Program.Container.GetInstance<IProdutoAppService>())
                {
                    _produtoView = appServiceProduto.ObterPorId(produtoId);
                    if (_produtoView != null)
                    {
                        txtPesquisa.Text = _produtoView.Descricao;
                        txtQuantidade.Focus();
                    }
                    else
                    {
                        TouchMessageBox.Show("Produto não encontrado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IniciaPesquisa();
                    }
                }
            }
        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal quantidade;
                decimal.TryParse(txtQuantidade.Text, out quantidade);
                if (quantidade > 0)
                {
                    _vendaFichaView = new VendaFichaViewModel();
                    _vendaFichaView.ProdutoId = _produtoView.ProdutoId;
                    _vendaFichaView.NomeProduto += _produtoView.Descricao;
                    _vendaFichaView.Quantidade = quantidade;
                    _vendaFichaView.ValorUnitatio = _produtoView.ValorVenda;
                    _vendaFichaView.ValorTotal = (_vendaFichaView.ValorUnitatio * _vendaFichaView.Quantidade);
                    _vendaFichaView.ClienteFichaId = _fichaView.ClienteFichaId;
                    _vendaFichaView.VendedorId = _vendedorView.VendedorId;

                    AdicionaItenGrid(_vendaFichaView);

                    IniciaPesquisa();
                }
            }
        }

        private void IniciaPesquisa()
        {
            txtPesquisa.Clear();
            txtQuantidade.Clear();
            _produtoView = null;
            txtPesquisa.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Complementos

            using (var appServiceComplemento = Program.Container.GetInstance<IProdutoComplementoAppService>())
            {
                _listComplementoViewModels =
                    appServiceComplemento.ObterPorGrupoId(int.Parse(_grupoProduto.ToString())).ToList();
            }


            if (_listComplementoViewModels.Count > 0)
            {
                splitContainer1.Panel2Collapsed = false;
                flpComplementos.Controls.Clear();

                foreach (ProdutoComplementoViewModel t in _listComplementoViewModels)
                {
                    var btnProdComplemento = new XButton()
                    {
                        Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular),
                        Theme = Theme.MSOffice2010_Publisher,
                        ForeColor = Color.Black,
                        Height = 53,
                        Width = 95
                    };
                    btnProdComplemento.Text = t.Descricao;
                    btnProdComplemento.CodigoProduto = t.ComplementoId;
                    btnProdComplemento.ValorUnitario = t.Valor;

                    btnProdComplemento.Click += BtnComplemento_Click;

                    flpComplementos.Controls.Add(btnProdComplemento);
                }

            }
            #endregion
        }
    }
}
