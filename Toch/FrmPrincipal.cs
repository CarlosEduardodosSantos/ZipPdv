using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Toch
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        clsProdutos produtos;
        List<CadMesas> ListMesas;
        CadGarcom cadgarcom = new CadGarcom();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmMesaItens frm = new FrmMesaItens();
            frm.ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer2.Panel2Collapsed = true;
            this.WindowState = FormWindowState.Maximized;
            IniciaMesas();
            toolStripTextBox1.Focus();
            timer1.Start();
        }

        public void IniciaMesas()
        {
            produtos = new clsProdutos();
            ListMesas = new List<CadMesas>();
            int Linhas;
            try
            {
                pnlMesas.Controls.Clear();
                pnlFechamentos.Controls.Clear();
                splitContainer2.Panel2Collapsed = true;

                ListMesas = produtos.ListaMesas();

                Linhas = ListMesas.Count;
                Linhas++;

                int i = 0; //espaço left
                int x = (pnlMesas.Width) / 122;
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
                        TextBoxProd btn = new TextBoxProd();
                        btn.Left = 125;
                        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                        btn.ForeColor = Color.White;

                        btn.Text = ListMesas[LinhaAtual - 1].Descricao;
                        //btn.Image = global::Toch.Properties.Resources._1343854489_table_furniture;
                        btn.CodigoProduto = ListMesas[LinhaAtual - 1].IdMesa;
                        btn.Parent = pnlMesas;
                        btn.Left = btn.Left * i + 5;
                        btn.Top = 4;
                        btn.Top = btn.Top + (53 * j);
                        btn.Height = 53;
                        btn.Width = 122;
                        btn.BackColor = CorStatus(ListMesas[LinhaAtual - 1].Situacao);
                        btn.Click += Btn_Click;

                        LinhaAtual++;
                        if (Linhas == LinhaAtual) return;

                        i++;
                    }
                    j++;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Soluções VIP");
            }
            finally
            {
                AtualizaStatusMesa();
                btnLogin.PerformClick();
                toolStripTextBox1.Focus();
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            TextBoxProd btnMesa = (TextBoxProd)sender;
            FrmMesaItens frm = new FrmMesaItens();
            clsMesa opmesa = new clsMesa();

            #region Função para fazer transferencia de mesas
            if (transf)
            {
                if (pnlOrigem.Controls.Count == 0)
                {
                    if (btnMesa.BackColor == Color.Red)
                    {
                        btnMesa.Parent = pnlOrigem;
                        btnMesa.Dock = DockStyle.Fill;
                        TouchMessageBox.Show("Selecione a mesa de Destino.", "Transferencia de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        TouchMessageBox.Show("Mesas Disponíveis ou Bloqueadas, não podem ser transferidas..", "Transferencia de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (pnlDestino.Controls.Count == 0)
                {
                    if (btnMesa.BackColor == Color.Goldenrod)
                    {
                        TouchMessageBox.Show("Mesa bloqueada! Não pode ser tranferida.", "Transferencia de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        btnMesa.Parent = pnlDestino;
                        btnMesa.Dock = DockStyle.Fill;
                    }
                }
                return;
            }
#endregion

            if (btnMesa.BackColor == Color.Green)
            {
                if (TouchMessageBox.Show("Deseja Abrir a Mesa?", "Abertura de mesa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    opMesa1 mesa = new opMesa1();
                    FrmSelecionaGarcom frmGarcom = new FrmSelecionaGarcom();
                    frmGarcom.IdGarcom = cadgarcom.IdGarcom;
                    frmGarcom.ShowDialog();
                    cadgarcom = new CadGarcom();
                    if (frmGarcom.cadGarcom == null)
                        return;

                    cadgarcom = frmGarcom.cadGarcom;
                    mesa.IdMesa = btnMesa.CodigoProduto;
                    mesa.Status = "A";
                    mesa.dthrInicial = DateTime.Now;
                    mesa.QtdePessoas = (Int16)frmGarcom.qtde;
                    mesa.IdGarcom = cadgarcom.IdGarcom;

                    opmesa.AbreMesa(mesa);

                    frm.IdMesa = btnMesa.CodigoProduto;
                    frm.DescricaoMesa = btnMesa.Text;
                    frm.garcom = cadgarcom;
                    frm.ShowDialog();
                }
                else
                    return;
            }
            else
            {
                frm.IdMesa = btnMesa.CodigoProduto;
                frm.DescricaoMesa = btnMesa.Text;
                if (cadgarcom == null)
                    btnLogin.PerformClick();
                else
                    frm.garcom = cadgarcom;
                frm.ShowDialog();
            }
            clsMesa ItensMesa = new clsMesa();
            List<opMesa2>  ListaItensMesa = new List<opMesa2>();

            //Verifica se exitem itens lancado na mesa. Caso nao tenha ele fecha a mesa
            ListaItensMesa = ItensMesa.ListaItensMesas(frm.opMesa.IdopMesa1);
            if (ListaItensMesa.Count == 0)
                opmesa.AlteraStatus("B", frm.opMesa.IdopMesa1);

            
            if (Program.UsaLoginMesa == 1)
                btnLogin.PerformClick();
            toolStripTextBox1.Clear();
            toolStripTextBox1.Focus();
            AtualizaStatusMesa();
        }

        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {

        }

        private Color CorStatus(string Situacao)
        {

            if (Situacao == "A")
                return Color.Red;
            if (Situacao == "L")
                return Color.Green;
            if (Situacao == "F")
                return Color.Goldenrod;
            if (Situacao == "")
                return Color.Green;
            
            return Color.Green;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                AtualizaStatusMesa();
            }
            catch (Exception ex)
            {
                TouchMessageBox.Show("Err: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Função para atualizar status da mesa Fica dentro de um loop
        /// </summary>
        private void AtualizaStatusMesa()
        {
            ListMesas = new List<CadMesas>();
            produtos = new clsProdutos();
            CadMesas mesa = new CadMesas();
            ListMesas = produtos.ListaMesas();
            bool bool_Mesas = false;
            txtMesasDisponiveis.Text = ListMesas.Count(P => P.Situacao == "L" || P.Situacao == " ").ToString() + " Disponiveis";
            txtMesasOcupadas.Text = ListMesas.Count(P => P.Situacao == "A").ToString() + " Ocupadas";
            txtMesasBloqueadas.Text = ListMesas.Count(P => P.Situacao == "F").ToString() + " Bloqueadas";

            foreach (TextBoxProd btn in pnlMesas.Controls)
            {
                int i = ListMesas.IndexOf(ListMesas.Find(x => x.IdMesa == btn.CodigoProduto));
                btn.BackColor = CorStatus(ListMesas[i].Situacao);
               
                if (btn.BackColor == Color.Red)
                {
                    btn.DataUltimoAtendimento =  produtos.MesaSemAtendimento(btn.CodigoProduto);
                    if (DateTime.Now.Subtract(btn.DataUltimoAtendimento).Minutes > 10)
                    {
                        foreach (TextBoxProd btnPesq in pnlFechamentos.Controls)
                        {
                            if (btnPesq.CodigoProduto == btn.CodigoProduto)
                            {
                                bool_Mesas = true;
                                break;
                            }
                        }
                        if (!bool_Mesas)
                        {
                            TextBoxProd prodSugest = new TextBoxProd();
                            prodSugest.DataUltimoAtendimento = btn.DataUltimoAtendimento;
                            prodSugest.Font = btn.Font;
                            prodSugest.ForeColor = btn.ForeColor;
                            prodSugest.BackColor = btn.BackColor;
                            prodSugest.Text = btn.Text;
                            prodSugest.Width = btn.Width;
                            prodSugest.Height = btn.Height;
                            prodSugest.Click += Btn_Click;
                            prodSugest.CodigoProduto = btn.CodigoProduto;
                            prodSugest.Parent = pnlFechamentos;
                            splitContainer2.Panel2Collapsed = false;
                        }

                    }
            
                }
        
            }
            //Devolve as mesas que ja foram atendidas
            foreach (TextBoxProd btn in pnlFechamentos.Controls)
            {
                btn.DataUltimoAtendimento = produtos.MesaSemAtendimento(btn.CodigoProduto);
                if (DateTime.Now.Subtract(btn.DataUltimoAtendimento).Minutes <= 10)
                {
                    pnlFechamentos.Controls.Remove(btn);
                    
                    if (pnlFechamentos.Controls.Count == 0)
                        splitContainer2.Panel2Collapsed = true;
                }
            }
            RefreshControls();
        }

        private void RefreshControls()
        {
            int i = 0;
            foreach (TextBoxProd btn in pnlFechamentos.Controls)
            {
                btn.Top = 0;
                btn.Left = i * 126;
                i++;
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 IdMesa = 0;
                if (Int32.TryParse(toolStripTextBox1.Text, out IdMesa))
                foreach (TextBoxProd btn in pnlMesas.Controls)
                {
                    if (btn.CodigoProduto == IdMesa)
                    {
                        Btn_Click(btn, e);
                        return;
                    }
                }
                foreach (TextBoxProd btn in pnlFechamentos.Controls)
                {
                    if (btn.CodigoProduto == IdMesa)
                    {
                        Btn_Click(btn, e);
                        return;
                    }
                }
            }
        }

        //Variavel para setar quando é uma transferencia de mesa
        bool transf = false;
        private void btnTransfereMesa_Click(object sender, EventArgs e)
        {
            TouchMessageBox.Show("Selecione a mesa de origem.", "Transferencia de Mesa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            splitContainer1.Panel2Collapsed = false;
            transf = true;
        }

        /// <summary>
        /// Transferencia de mesa função para confirmar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            clsMesa cmsMesa = new clsMesa();
            Auditoria auditoria = new Auditoria();
            int IdMesaOrigem = 0, IdMesadestino = 0;

            foreach (TextBoxProd btnTransf in pnlOrigem.Controls)
                IdMesaOrigem = btnTransf.CodigoProduto;

            foreach (TextBoxProd btnTransf in pnlDestino.Controls)
            {
                IdMesadestino = btnTransf.CodigoProduto;
                if (btnTransf.BackColor == Color.Green)
                {
                    if (TouchMessageBox.Show("Deseja Abrir a Mesa?", "Abertura de mesa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        opMesa1 mesa = new opMesa1();
                        FrmSelecionaGarcom frmGarcom = new FrmSelecionaGarcom();
                        frmGarcom.IdGarcom = cadgarcom.IdGarcom;
                        frmGarcom.ShowDialog();
                        mesa.IdMesa = IdMesadestino;
                        mesa.Status = "A";
                        mesa.dthrInicial = DateTime.Now;
                        mesa.QtdePessoas = (Int16)frmGarcom.qtde;
                        mesa.IdGarcom = frmGarcom.cadGarcom.IdGarcom;
                        mesa.MesaTransf = IdMesaOrigem;
                        cmsMesa.AbreMesa(mesa);
                    }
                    else
                        return;
                }
            }
            cmsMesa.TransfereMesa(IdMesaOrigem, IdMesadestino);
            splitContainer1.Panel2Collapsed = true;
            transf = false;
            pnlDestino.Controls.Clear();
            pnlOrigem.Controls.Clear();

            auditoria.cliente = 0;
            auditoria.data = DateTime.Now;
            auditoria.hora = DateTime.Now;
            auditoria.loja = 1;
            auditoria.maquina = 1;
            auditoria.motivo = string.Empty;
            auditoria.NROCX = 0;
            auditoria.usuario = cadgarcom.IdGarcom;
            auditoria.Venda = 0;
            auditoria.valor = 0;
            auditoria.ocorrencia = "[TRANSFERÊNCIA GERAL MESA ORI: " +IdMesaOrigem + " MESA DEST:" +  IdMesadestino + "]";
            cmsMesa.GravaAuditoria(auditoria);
            IniciaMesas();
        }

        private void btnCancelarTransf_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            transf = false;
            pnlDestino.Controls.Clear();
            pnlOrigem.Controls.Clear();
            IniciaMesas();
        }

        private void btnChamaTecladoNumerico_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = FrmTecladoNumerico.TecladoNumerico(0, "Entre com o número da mesa.");
            KeyEventArgs key = new KeyEventArgs(Keys.Enter);
            toolStripTextBox1_KeyDown(sender, key);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            cadgarcom = new CadGarcom();
            //toolStripLabel2.Text = string.Empty;
            btnLogin.Text = "Login";
            clsMesa cmsMesa = new clsMesa();
            string senha = FrmTecladoNumerico.TecladoNumerico(1, "Entre com sua senha de acesso.");
            cadgarcom = cmsMesa.ListagarcomId(0, senha);
            if (cadgarcom == null)
            {
                TouchMessageBox.Show("Senha não cadastrada. Tente Novamente.", "Acesso ao sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin_Click(senha, e);
            }
            else
            {
                //toolStripLabel2.Text = cadgarcom.Descricao;
                btnLogin.Text = "Logoff/" + cadgarcom.Descricao;
            }
        }
    }
}
