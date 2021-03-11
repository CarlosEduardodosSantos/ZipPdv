using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Toch
{
    public partial class FrmMesaItens : Form
    {
        public FrmMesaItens()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;
        }
        public int IdMesa { get; set; }
        public int IdGarcom { get; set; }
        public string DescricaoMesa { get; set; }
        public int ItensLancados { get; set; }
        public CadGarcom garcom;
        clsProdutos produtos;
        clsMesa ItensMesa;
        List<GRUPO> ListaGrupo;
        List<PROD> ListaProdutos;
        public List<opMesa2> ListaItensMesa;
        List<clsMesaItens> ListaItens;
        List<Complemento> ListaComponente;
        List<MeioMeio> ListaMeioMeio;
        

        VENDA_4 venda4;
        /// <summary>
        /// Venda 4 armazena as informações dos complementos
        /// </summary>
        List<VENDA_4> ListaVenda4 = new List<VENDA_4>();
        public opMesa1 opMesa;

        int GrupoProduto;

        private void FrmMesaItens_Load(object sender, EventArgs e)
        {
            produtos = new clsProdutos();
            //Carrega Informações da mesa
            ListaItens = new List<clsMesaItens>();
            ListaMeioMeio = new List<MeioMeio>();
            opMesa = new opMesa1();
            opMesa = produtos.opaMesas(IdMesa);
            opMesa.IdGarcom = garcom.IdGarcom;
            //Mesa Itens
            ItensMesa = new clsMesa();
            ListaItensMesa = new List<opMesa2>();


            ListaItensMesa = ItensMesa.ListaItensMesas(opMesa.IdopMesa1);

            inclanc = ListaItensMesa.Count > 0 ? (ListaItensMesa.Max(P => (int)P.SEQLANC) + 1) : 1;

            lbParcial.Text = "R$ " + ItensMesa.PagamentoParcial(opMesa.IdopMesa1).ToString("N2");
            lbGarcom.Text = garcom.Descricao;
            lbDataAbertura.Text = opMesa.dthrInicial.Value.TimeOfDay.ToString();
            lbDescricaoMesa.Text = DescricaoMesa;
            CarregaItensMesa(ListaItensMesa);

            lbConferidos.Text = ListaItensMesa.Count.ToString();

            Refresh(sender, e);

            if (opMesa.Status == "F")
            {
                groupGrupo.Enabled = false;
                groupProdutos.Enabled = false;
                btnGravarItens.Enabled = false;
                lbMesaBloqueada.Text = "Mesa Bloqueada";
            }

        }

        public void Refresh(object sender, EventArgs e)
        {
            System.Drawing.Color BackColor;
            System.Drawing.Color Color1 = System.Drawing.Color.White;
            System.Drawing.Color Color2 = System.Drawing.Color.White;
            BackColor = Color1;
            pnlGrupo.HorizontalScroll.Enabled = false;
            ListaGrupo = new List<GRUPO>();
            int Linhas;
            ListaGrupo = produtos.ListaGrupo();
            if (ListaGrupo.Count > 0)
            {
                Linhas = ListaGrupo.Count;
                Linhas++;
                int i = 0; //espaço left
                int x = (pnlGrupo.Width)/ 98;
                int j = 0;
                int LinhaAtual = 1;
                bool BreakOut = false;
                int y = Linhas / x; //Verifica quantas linha tera

                //Verifica se o resultado é fracionado e adiciona mais uma linha
                double Valor = ((double)Linhas / (double)x);
                if (Valor - Math.Truncate(Valor) > 0) y++;


                while (j < y)
                {
                    i = 0;
                    if (j % 2 == 0)
                        BackColor = Color2;
                    else
                        BackColor = Color1;

                    while (i < x)
                    {
                        TextBoxProd btn = new TextBoxProd();
                        btn.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
                        btn.ForeColor = System.Drawing.Color.Black;
                        btn.BackColor = BackColor;
                        
                        btn.Left = 98;
                        btn.Text = ListaGrupo[LinhaAtual - 1].DES_;
                        btn.CodigoProduto = ListaGrupo[LinhaAtual - 1].GRUPO1;
                        GrupoProduto = ListaGrupo[LinhaAtual - 1].GRUPO1;
                        btn.Parent = pnlGrupo;
                        btn.Left = btn.Left * i + 5;
                        btn.Top = 4;
                        btn.Top = btn.Top + (53 * j);
                        btn.Height = 53;
                        btn.Width = 95;
                        btn.Click += Btn_Click;
                        //btn.DoubleClick += new EventHandler(BtnComplemento_DoubleClick);

                        LinhaAtual++;
                        if (Linhas == LinhaAtual) { BreakOut = true; break; };

                        i++;
                    }
                    j++;
                    if (BreakOut)
                        break;
                }
            }
            else
                TouchMessageBox.Show("Grupo do PDV não cadastrado.", "E-Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);

            splitContainer1.Panel2Collapsed = true;
            dataGridView1.Select();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            groupProdutos.Enabled = true;
            pnlProd.Controls.Clear();
            pnlComplementos.Controls.Clear();

            produtos = new clsProdutos();
            ListaProdutos = new List<PROD>();
            int Linhas;
            GrupoProduto = ((TextBoxProd)sender).CodigoProduto;
            groupProdutos.Text = "Produtos";
            if (GrupoProduto == 0) return;

            ListaProdutos = produtos.ListaProdutosGrupo(GrupoProduto.ToString());
            if (ListaProdutos.Count > 0)
            {
                Linhas = ListaProdutos.Count;
                Linhas++;

                if (ListaProdutos.Count == 0) return;
                int i = 0; //espaço left
                int x = pnlProd.Width / 95;
                int j = 0;
                int LinhaAtual = 1;
                int y = Linhas / x; //Verifica quantas linha tera

                //Verifica se o resultado é fracionado e adiciona mais uma linha
                double Valor = ((double)Linhas / (double)x);
                if (Valor - Math.Truncate(Valor) > 0) y++;

                while (j < y)
                {
                    i = 0;
                    while (i < x)
                    {
                        TextBoxProd BtnProd = new TextBoxProd();
                        BtnProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
                        BtnProd.BackColor = ((TextBoxProd)sender).BackColorEnter;  
                        BtnProd.ForeColor = System.Drawing.Color.Black;
                        BtnProd.Left = 95;
                        BtnProd.Text = ListaProdutos[LinhaAtual - 1].DES_;
                        BtnProd.Parent = pnlProd;
                        BtnProd.Left = BtnProd.Left * i + 5;
                        BtnProd.Top = 4;
                        BtnProd.Top = BtnProd.Top + (53 * j);
                        BtnProd.Height = 53;
                        BtnProd.Width = 95;
                        BtnProd.CodigoProduto = ListaProdutos[LinhaAtual - 1].CODIGO;
                        BtnProd.ValorUnitario = ListaProdutos[LinhaAtual - 1].VLVENDA.HasValue ? (decimal)ListaProdutos[LinhaAtual - 1].VLVENDA : 0;
                        BtnProd.TipoProduto = ListaProdutos[LinhaAtual - 1].TIPO;
                        BtnProd.Click += BtnProd_Click;
                        BtnProd.DoubleClick += new EventHandler(BtnComplemento_DoubleClick);


                        LinhaAtual++;
                        if (Linhas == LinhaAtual) return;

                        i++;
                    }

                    j++;

                }
            }
            
        }

        List<clsMesaItens> List = new List<clsMesaItens>();
        
        clsMesaItens itens;
        private void BtnProd_Click(object sender, EventArgs e)
        {
            TextBoxProd btnProd = (TextBoxProd)sender;

            if (btnProd.TipoProduto == "M")
            {
                //FrmMeioMeio frm = new FrmMeioMeio();
                //frm.ShowDialog();
                groupGrupo.Enabled = false;
                splitContainer1.Panel2Collapsed = true;
                pnlProd.Controls.Clear();
                pnlComplementos.Controls.Clear();

                groupProdutos.Text = "Produtos Meio a Meio";

                produtos = new clsProdutos();
                ListaProdutos = new List<PROD>();
                int Linhas;

                if (GrupoProduto == 0) return;

                ListaProdutos = produtos.ListaProdutosMeioMeio();
                if (ListaProdutos.Count > 0)
                {
                    Linhas = ListaProdutos.Count;
                    Linhas++;

                    if (ListaProdutos.Count == 0) return;
                    bool FimProd = false;
                    int i = 0; //espaço left
                    int x = pnlProd.Width / 95;
                    int j = 0;
                    int LinhaAtual = 1;
                    int y = Linhas / x; //Verifica quantas linha tera

                    //Verifica se o resultado é fracionado e adiciona mais uma linha
                    double Valor = ((double)Linhas / (double)x);
                    if (Valor - Math.Truncate(Valor) > 0) y++;

                    while (j < y)
                    {
                        i = 0;
                        while (i < x)
                        {
                            TextBoxProd BtnProd = new TextBoxProd();
                            BtnProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
                            BtnProd.BackColor = ((TextBoxProd)sender).BackColorEnter;
                            BtnProd.ForeColor = System.Drawing.Color.Black;
                            BtnProd.Left = 95;
                            BtnProd.Text = ListaProdutos[LinhaAtual - 1].DES_;
                            BtnProd.Parent = pnlProd;
                            BtnProd.Left = BtnProd.Left * i + 5;
                            BtnProd.Top = 4;
                            BtnProd.Top = BtnProd.Top + (53 * j);
                            BtnProd.Height = 53;
                            BtnProd.Width = 95;
                            BtnProd.CodigoProduto = ListaProdutos[LinhaAtual - 1].CODIGO;
                            BtnProd.ValorUnitario = ListaProdutos[LinhaAtual - 1].VLVENDA.HasValue ? (decimal)ListaProdutos[LinhaAtual - 1].VLVENDA : 0;
                            BtnProd.TipoProduto = ListaProdutos[LinhaAtual - 1].TIPO;
                            BtnProd.Click += BtnMeioMeio_Click;


                            LinhaAtual++;
                            if (Linhas == LinhaAtual) { FimProd = true; break; };

                            i++;
                        }

                        j++;
                        if (FimProd)
                            break;
                    }
                }
            }


            ListaComponente = new List<Complemento>();
            ListaComponente = produtos.ListaComplemento(GrupoProduto);

            if (ListaComponente.Count > 0)
            {
                /*
                splitContainer1.Panel2Collapsed = false;
                itens = new clsMesaItens();
                TextBoxProd btnProd = (TextBoxProd)sender;
                itens.Codigo = btnProd.CodigoProduto;
                itens.Descricao = btnProd.Text;
                itens.Qtde = 1;
                itens.Unitario = btnProd.ValorUnitario;
                itens.Valor = (itens.Unitario * itens.Qtde);
                lbDesProd.Text = itens.Descricao;
                */
                splitContainer1.Panel2Collapsed = false;
                pnlComplementos.Controls.Clear();
                int Linhas;
                bool FimProd = false;
                Linhas = ListaComponente.Count;
                Linhas++;
                int i = 0; //espaço left
                int x = pnlComplementos.Width / 85;
                int j = 0;
                int LinhaAtual = 1;
                int y = Linhas / x; //Verifica quantas linha tera

                //Verifica se o resultado é fracionado e adiciona mais uma linha
                double Valor = ((double)Linhas / (double)x);
                if (Valor - Math.Truncate(Valor) > 0) y++;

                while (j < y)
                {
                    i = 0;
                    while (i < x)
                    {
                        TextBoxProd BtnProd = new TextBoxProd();
                        BtnProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
                        BtnProd.ForeColor = System.Drawing.Color.Black;
                
                        BtnProd.Left = 85;
                        BtnProd.Text = ListaComponente[LinhaAtual - 1].des_;
                        BtnProd.Parent = pnlComplementos;
                        BtnProd.Left = BtnProd.Left * i + 5;
                        BtnProd.Top = 4;
                        BtnProd.Top = BtnProd.Top + (53 * j);
                        BtnProd.Height = 53;
                        BtnProd.Width = 85;
                        BtnProd.CodigoProduto = ListaComponente[LinhaAtual - 1].inc_compto;
                        BtnProd.ValorUnitario = ListaComponente[LinhaAtual - 1].valor.HasValue ? (decimal)ListaComponente[LinhaAtual - 1].valor : 0;
                        BtnProd.Click += BtnComplemento_Click;

                        LinhaAtual++;
                        if (Linhas == LinhaAtual) { FimProd = true; break; };

                        i++;
                    }

                    j++;
                    if (FimProd)
                        break;
                }

            }
     
            {
                itens = new clsMesaItens();
                itens.Codigo = btnProd.CodigoProduto;
                itens.Descricao += btnProd.Text;
                itens.Qtde = 1;
                itens.Unitario = btnProd.ValorUnitario;
                itens.Valor = (itens.Unitario * itens.Qtde);

                AdicionaItenGrid(itens);
            }



        }
        decimal ValorProdutos;
        private void BtnMeioMeio_Click(object sender, EventArgs e)
        {

            TextBoxProd btnProd = (TextBoxProd)sender;

            DataGridViewSelectedRowCollection theRowsSelected = this.dataGridView1.SelectedRows;
            if (theRowsSelected.Count > 0)
            {

                itens = ListaItens[theRowsSelected[0].Index];
                MeioMeio itemMeio = new MeioMeio();
                
                if (itens.IdMeio1 == 0)
                {

                    itemMeio.Descricao = btnProd.Text;
                    itemMeio.IdProduto = btnProd.CodigoProduto;
                    itemMeio.ValorVenda = btnProd.ValorUnitario;
                    itemMeio.SeqProduto = itens.SeqLanc;
                    itemMeio.NroOperacao = itens.IdopMesa1;
                    itemMeio.Quantidade = 1;
                    itemMeio.QtdeImpresso = "1/2";
                    ListaMeioMeio.Add(itemMeio);

                    ValorProdutos = btnProd.ValorUnitario;
                    itens.IdMeio1 = btnProd.CodigoProduto;
                    itens.Descricao = "1/2 " + btnProd.Text;
                }
                else if (itens.IdMeio2 == 0)
                {
                    itemMeio.Descricao = btnProd.Text;
                    itemMeio.IdProduto = btnProd.CodigoProduto;
                    itemMeio.ValorVenda = btnProd.ValorUnitario;
                    itemMeio.SeqProduto = itens.SeqLanc;
                    itemMeio.NroOperacao = itens.IdopMesa1;
                    itemMeio.Quantidade = 1;
                    itemMeio.QtdeImpresso = "1/2";
                    ListaMeioMeio.Add(itemMeio);

                    itens.IdMeio2 = btnProd.CodigoProduto;
                    itens.Descricao += "1/2 " + btnProd.Text;
                    TextBoxProd refresh = new TextBoxProd();
                    refresh.CodigoProduto = GrupoProduto;
                    groupProdutos.Enabled = false;
                    groupGrupo.Enabled = true;
                    if (ValorProdutos > btnProd.ValorUnitario)
                        itens.Valor = ValorProdutos;
                    else
                        itens.Valor = btnProd.ValorUnitario;
                    //Btn_Click(refresh, new EventArgs());
                }
                AtualizaGrid();
            }
            else
            {
                TextBoxProd refresh = new TextBoxProd();
                refresh.CodigoProduto = GrupoProduto;
                groupGrupo.Enabled = true;
                Btn_Click(refresh, new EventArgs());
            }
        }

        private void BtnComplemento_Click(object sender, EventArgs e)
        {

            TextBoxProd btnProd = (TextBoxProd)sender;

            venda4 = new VENDA_4();
            venda4.IDOPMESA1 = opMesa.IdMesa;
            venda4.PRODCOD = itens.Codigo;
            venda4.COMPCOD = btnProd.CodigoProduto;
            venda4.VALOR = btnProd.ValorUnitario;

            DataGridViewSelectedRowCollection theRowsSelected = this.dataGridView1.SelectedRows;

            itens = ListaItens[theRowsSelected[0].Index];
            itens.IdComplemento = btnProd.CodigoProduto;
            itens.Descricao += " + " + btnProd.Text;
            itens.Qtde = 1;
            itens.Unitario += (decimal)venda4.VALOR;
            itens.Valor = (itens.Unitario * itens.Qtde);
            AtualizaGrid();
            //lbDesProd.Text = itens.Descricao;
            //if (TouchMessageBox.Show("Adicionar mais complemento?", "Itens Mesa.",
            //        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            //    btnIncluirIten.PerformClick();

        }

        private void AtualizaGrid()
        {
            ListaItens = new List<clsMesaItens>();
            index = 0;
            foreach (clsMesaItens itens in List)
            {
                ListaItens.Add(itens);
                index++;
            }
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ListaItens;

            if (dataGridView1.Rows.Count > 0 && index >= 0)
                dataGridView1.Rows[index-1].Selected = true;


            

            lbTotal.Text = "R$ " + ListaItens.Sum(P => P.Valor).ToString("N2");
            blPendentes.Text = ListaItens.Count.ToString();
        }

        private void CarregaItensMesa(List<opMesa2> _ListaItens)
        {
            ItensMesa = new clsMesa();
            dgvItensMesa.AutoGenerateColumns = false;
            dgvItensMesa.DataSource = _ListaItens;
            lbValorMesaTotal.Text = "R$ " + ListaItensMesa.Sum(P => P.Valor).ToString();
        }

        private void btnIncluirIten_Click(object sender, EventArgs e)
        {
            AdicionaItenGrid(itens);
            foreach (TextBoxProd btn in pnlGrupo.Controls)
            {
                if (btn.CodigoProduto == GrupoProduto)
                    Btn_Click(btn, e);
            }
            //lbDesProd.Text = string.Empty;
        }

        private void btnGravarItens_Click(object sender, EventArgs e)
        {
            clsMesa cls = new clsMesa();
            clsProdutos clsmesas = new clsProdutos();
            opMesa1 VerificaMesa = new opMesa1();

            //Verifica se mesa realmente esta aberto (Verificação nessesaria.)
            VerificaMesa = clsmesas.opaMesas((int)opMesa.IdMesa);
            if (VerificaMesa.Status != "A")
                cls.AlteraStatus("A", (int)opMesa.IdopMesa1);

            foreach (clsMesaItens itensMesa in ListaItens)
            {
                opMesa2 opmesa2 = new opMesa2();
                opmesa2.IdopMesa1 = opMesa.IdopMesa1;
                opmesa2.idpromocao = itensMesa.IdPromocao;
                opmesa2.Qtde = itensMesa.Qtde;
                opmesa2.SEQLANC = itensMesa.SeqLanc;
                opmesa2.Status = false;
                opmesa2.Valor = itensMesa.Valor;
                opmesa2.vlunit = itensMesa.Unitario;
                opmesa2.SABOR = 0;
                opmesa2.DesProduto = itensMesa.Descricao;
                opmesa2.CodProduto = itensMesa.Codigo;
                opmesa2.cod_garcom = opMesa.IdGarcom;
                opmesa2.Meio1 = 0;
                opmesa2.Meio2 = 0;
                opmesa2.PROD_OBS = !string.IsNullOrEmpty(itensMesa.Observacao) ? itensMesa.Observacao : "";
                try
                {
                    cls.GravaItensMesa(opmesa2);
                }
                catch
                {
                    TouchMessageBox.Show("Erro ao lançar o produto: " + itensMesa.Descricao, "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            foreach (VENDA_4 comp in ListaVenda4)
            {
                try
                {
                    comp.IDOPMESA1 = opMesa.IdopMesa1;
                    cls.GravaItensVENDA_4(comp);
                }
                catch
                {
                    TouchMessageBox.Show("Erro ao lançar o Complemento.", "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            foreach (MeioMeio meio in ListaMeioMeio)
            {
                try
                {
                    meio.NroOperacao = opMesa.IdopMesa1;
                    cls.GravaItenMeioMeio(meio);
                }
                catch
                {
                    TouchMessageBox.Show("Erro ao lançar o Meio a Meio.", "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                cls.Gr_Impressao(opMesa.IdopMesa1, 1);
            }
            catch
            {
                TouchMessageBox.Show("Erro Imprimimir.", "Lançamento mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            inclanc = 0;
            ItensLancados = ListaItens.Count;
            ListaMeioMeio.Clear();
            ListaVenda4.Clear();
            this.Close();
        }

        int index;
        private void btnAdicionarQtde_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow select in dataGridView1.SelectedRows)
            {
                index = select.Index;
                List[index].Qtde += 1;
                List[index].Valor = (List[index].Unitario * List[index].Qtde);
            }
            AtualizaGrid();
        }

        private void btnExcluirItens_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow select in dataGridView1.SelectedRows)
            {
                index = select.Index;

                if (List[index].Qtde == 1)
                {
                    foreach (VENDA_4 venda4 in ListaVenda4)
                    {
                        if (List[index].SeqLanc == venda4.SEQLANC)
                        {
                            ListaVenda4.Remove(venda4);
                            break;
                        }
                    }
                    List.RemoveAt(index);
                    index -= 1;
                }
                else
                {
                    List[index].Qtde -= 1;
                    List[index].Valor = (List[index].Unitario * List[index].Qtde);
                }

                select.Selected = false;

            }
            if (!groupGrupo.Enabled)
            {
                TextBoxProd refresh = new TextBoxProd();
                refresh.CodigoProduto = GrupoProduto;
                groupGrupo.Enabled = true;
                Btn_Click(refresh, new EventArgs());
            }
            AtualizaGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmObservacao frm = new FrmObservacao();
            index = e.RowIndex;
            frm.DescricaoProd = List[index].Descricao;
            frm.ShowDialog();
            List[index].Descricao = frm.DescricaoProd;

            AtualizaGrid();
        }
        int inclanc;
        private void AdicionaItenGrid(clsMesaItens _itens)
        {
            decimal ValorPromocao = 0;
            clsProdutos prdutos = new clsProdutos();

            decimal ValorMesa = prdutos.ValorProd(_itens.Codigo, ref ValorPromocao);
            itens.IdopMesa1 = opMesa.IdopMesa1;
            if (ValorPromocao > 0)
            {
                FrmPromocao frm = new FrmPromocao();
                frm.ValorPromocao = ValorPromocao;
                frm.ValorVenda = ValorMesa;
                frm.Produto = itens.Descricao;
                frm.ShowDialog();
                itens.Unitario = frm.ValorSelecionado;
            }

            {
                bool AddIntem = true;
                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].Codigo == itens.Codigo && List[i].Descricao == itens.Descricao)
                    {
                        List[i].Qtde += itens.Qtde;
                        List[i].Valor = (List[i].Qtde * List[i].Unitario);
                        AddIntem = false;
                        break;
                    }
                }
                if (AddIntem)
                {
                    itens.SeqLanc = inclanc;
                    if (venda4 != null)
                    {
                        venda4.SEQLANC = itens.SeqLanc;
                        ListaVenda4.Add(venda4);
                    }

                    itens.Valor = (itens.Unitario * itens.Qtde);

                    List.Add(itens);
                }
            }
            AtualizaGrid();
            inclanc++;
        }

        private void BtnComplemento_DoubleClick(object sender, EventArgs e)
        {
            if (ListaComponente.Count > 0)
            {
                TextBoxProd btnProd = (TextBoxProd)sender;
                itens = new clsMesaItens();
                itens.Codigo = btnProd.CodigoProduto;
                itens.Descricao += " + " + btnProd.Text;
                itens.Qtde = 1;
                itens.Unitario = btnProd.ValorUnitario;
                itens.Valor = (itens.Unitario * itens.Qtde);
                if (ListaItens != null)
                {
                    if (ListaItens.Exists(P => P.Descricao == itens.Descricao))
                        if (TouchMessageBox.Show("Produto ja existente. Deseja incluir?", "Produto ja lançado", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.No)
                            itens = new clsMesaItens();
                }
                AdicionaItenGrid(itens);

            }
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {

            if (opMesa.Status != "F" && ListaItens.Count > 0)
            {
                if (TouchMessageBox.Show("Os itens lançados não foram confirmados ainda! Confirma retorno ao mapa de mesas?", "Sair da Mesa",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    //opMesa = null;
                    List = null;
                    ListaComponente = null;
                    ListaGrupo = null;
                    ListaProdutos = null;
                    ItensLancados = 0;
                    this.Close();
                }
            }
            else
                this.Close();
        }

        private void btnObservacao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow select = dataGridView1.SelectedRows[0];
                index = select.Index;
                FrmObservacao frm = new FrmObservacao();
                frm.DescricaoProd = List[index].Descricao;
                frm.ShowDialog();

                List[index].Observacao = frm.Observacao;
                List[index].Descricao = frm.DescricaoProd;

                AtualizaGrid();
            }
        }
    }
}
