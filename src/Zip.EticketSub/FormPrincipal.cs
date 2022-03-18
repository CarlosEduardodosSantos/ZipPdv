using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using MobileAppServerClient;
using Zip.Sat;
using Zip.Utils;


namespace Zip.EticketSub
{
    public partial class FormPrincipal : Form
    {
        private readonly IVendaAppService _vendaAppService;
        private readonly IProdutoAppService _produtoAppService;
        private VendaViewModel _pedido;
        private int _passagem;
        public FormPrincipal()
        {
            InitializeComponent();
            _vendaAppService = Program.Container.GetInstance<IVendaAppService>();
            _produtoAppService = Program.Container.GetInstance<IProdutoAppService>();
            timer1.Tick += Timer1_Tick;
            lbEmpresa.Text = Program.EmpresaView.RazaoSocial;
            this.Load += FormPrincipal_Load1;
        }

        private void FormPrincipal_Load1(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Minimized;
            Hide();


            if (!string.IsNullOrEmpty(Global.ConfiguracaoInicial.SatServidor))
            {
                string servidor = Global.ConfiguracaoInicial.SatServidor;
                int porta = Global.ConfiguracaoInicial.PortaServidor;

                Client.Configure(servidor, porta, ((4096 * 100) * 1000));
                Client client = new Client();
                client.ByteBuffer = 38000;
                try
                {


                    RequestBody rb = RequestBody.Create("ServerInfoController", "FullServerInfo");
                    client.SendRequest(rb);

                    var result = client.GetResult(typeof(ServerInfo));
                    var serverInfo = (ServerInfo) result.Entity;

                    client.Close();


                    lbServidor.Text =
                        $"Zip Servidor, versão {serverInfo.ServerVersion}, iniciado em  {serverInfo.MachineName}";


                    // lbServidor.Text = $"Servidor {serverInfo.MachineName}";
                    lbStatus.Text = $"Status: Servidor em operação.";
                }
                catch (Exception exception)
                {
                    lbServidor.Text = $"Servidor {servidor}, {porta} - {exception.Message}";
                    lbStatus.Text = $"Status Inativo";
                    lbStatus.ForeColor = Color.Red;
                }
      

            }


            CarregaVendas();

        }

        private void CarregaVendas()
        {
            var dataInicio = DateTime.Now.Date;
            var dataFinal = DateTime.Now.Date;
            var pdv = Program.Pdv;
            var vendas = _vendaAppService.ObterPendenteSat(dataInicio, dataFinal, pdv).OrderByDescending(t => t.DataHora).Take(10);

            //flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            foreach (var vendaViewModel in vendas)
            {
                var item = new UControlVenda(vendaViewModel);
                item.EnviarItem += Item_EnviarItem;
                item.CancelarItem += Item_CancelarItem;
                item.Width = flowLayoutPanel2.Width - 15;
                flowLayoutPanel2.Controls.Add(item);

            }
        }

        private void Item_CancelarItem(object sender, EventArgs e)
        {
            var item = (UControlVenda)sender;
            var vendaView = item.VendaView;
            using (var formSat = new FrmCancelaSat(vendaView))
            {

                formSat.ShowDialog();
                var result = formSat.RetornoSatView;
                if (result.IsOk)
                {
                    _vendaAppService.Cancelar(vendaView, "");
                    CarregaVendas();
                }
                MessageBox.Show(result.Mensagem);
            }
        }

        private void Item_EnviarItem(object sender, EventArgs e)
        {
            var item = (UControlVenda)sender;
            var vendaView = item.VendaView;
            vendaView.VendaFinalizadora = new List<CaixaPagamentoViewModel>()
            {
                new CaixaPagamentoViewModel()
                {
                    CodigoFiscal = vendaView.TipoPagamento == "DB" ? "04" : "03",
                    Valor = vendaView.ValorTotal
                }
            };

            
            if (!string.IsNullOrEmpty(vendaView.Cnpj))
            {
                if (vendaView.Cnpj.Length < 14)
                {
                    var isCpfValido = Zip.Utils.ValidaCpfCnpj.ValidaCpf(vendaView.Cnpj);
                    if (!isCpfValido)
                    {
                        vendaView.Cnpj = MessageBox.Show("O CPF informado é invalido\nDeseja informar novamente?",
                                             "Validação",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                             MessageBoxDefaultButton.Button1) ==
                                         DialogResult.Yes
                            ? vendaView.Cnpj = FrmCpfCupom.Instance()
                            : "";
                    }
                }
                else
                {
                    vendaView.Cnpj = Utils.ValidaCpfCnpj.ValidarCNPJ(vendaView.Cnpj) ? vendaView.Cnpj : "";

                }
            }
            using (var formSat = new FrmSolicitaSat(vendaView))
            {
                formSat.Visible = false;
                formSat.ShowDialog();
                
                var result = formSat.RetornoSatView;
                using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                {
                    retornoSatAppService.Adicionar(result);
                }

                //if (result.IsOk)
                CarregaVendas();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            StreamReader sr = null;
            try
            {
                var files = VerificaArquivo.ConsultaArquivos(Program.PastaOperacao);
                timer1.Stop();

                for (var i = 0; i < files.Length; i++)
                {
                    var file = files[i];
                    var fileFrom = Path.Combine(Program.PastaOperacao, file);
                    var linhaTXT = string.Empty;

                    if (File.Exists(fileFrom))
                    {
                        _pedido = new VendaViewModel();
                        _pedido.Loja = Program.Loja;
                        _pedido.Pdv = Program.Pdv;
                        _pedido.UsuarioId = Program.VendedorId;
                        _pedido.CaixaId = Program.CaixaId;

                        sr = new StreamReader(fileFrom,
                            Encoding.GetEncoding("iso-8859-1"));

                        while (!sr.EndOfStream)
                        {
                            linhaTXT = sr.ReadLine();

                            if (linhaTXT == "") continue;

                            linhaTXT = Regex.Replace(linhaTXT, "[^0-9a-zA-Z-|]+", "");

                            DefineLinha(linhaTXT);
                        }

                        if (_pedido.VendaItens.Count > 0)
                        {
                            try
                            {

                                var pedidoId = int.Parse($"{Program.Pdv}{_vendaAppService.ObterVendaId() + 1}");
                                _pedido.VendaId = pedidoId;
                                _vendaAppService.Adicionar(_pedido);

                                //var venda = _vendaAppService.ObterPorId(pedidoId);

                                //Grava Caixa Itens
                                var caixaItem = new CaixaItemViewModel();
                                caixaItem.CaixaPagamentos = new List<CaixaPagamentoViewModel>()
                                {
                                    new CaixaPagamentoViewModel()
                                    {
                                        Interno = _pedido.TipoPagamento == "DB" ? "ESP3" : _pedido.TipoPagamento == "CC" ? "ESP5" : "ESP4",
                                        Especie = _pedido.TipoPagamento == "DB" ? "Cartão Debito":  _pedido.TipoPagamento == "CC" ? "Cartão Consumo" : "Cartão Credito",
                                        Valor = _pedido.ValorTotal
                                    }
                                };
                                _pedido.VendaFinalizadora = new List<CaixaPagamentoViewModel>()
                                {
                                    new CaixaPagamentoViewModel()
                                    {
                                        CodigoFiscal = _pedido.TipoPagamento == "DB" ? "04" : _pedido.TipoPagamento == "CC" ? "99": "03",
                                        Valor = _pedido.ValorTotal
                                    }
                                };
                                try
                                {
                                    using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
                                    {
                                        caixaItem.VendaId = pedidoId;
                                        caixaItem.UsuarioId = Program.VendedorId;
                                        caixaItem.Historico = $"VENDA Nº {pedidoId}";
                                        caixaItem.TipoLancamento = "VDA";
                                        caixaApp.Adicionar(caixaItem);
                                    }
                                }
                                catch (Exception eCaixa)
                                {
                                    throw new Exception("Erro na função Grava Caixa: " + eCaixa.Message);
                                }


                                //Envia SAT caso esteja configurado se não, manda para o servidor uma solicitação de SAT
                                using (var formSat = new FrmSolicitaSat(_pedido))
                                {
                                    formSat.Visible = false;
                                    formSat.ShowDialog();

                                    var retornoSat = formSat.RetornoSatView;
                                    using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                                    {
                                        retornoSatAppService.Adicionar(retornoSat);
                                    }

                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);

                                //Ignore
                            }
                            finally
                            {

                                try
                                {
                                    sr.Close();
                                    var to = Path.Combine(Program.PastaConcluidos, file);
                                    if (File.Exists(to))
                                        File.Delete(to);


                                    File.Move(fileFrom, to); // Try to move

                                    CarregaVendas();

                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine(ex); // Write error
                                }
                            }

                        }
                    }
                }

                //Verifica vendas que deram algum tipo de erro 
                /*if (flowLayoutPanel2.Controls.Count > 0)
                    RepassaVendas();*/


            }
            catch (Exception exception)
            {
                //Ignore

                new LogWriter(exception.Message);
            }
            finally
            {
                //CarregaVendas();
                timer1.Start();
            }


        }

        private void DefineLinha(string linhaTxt)
        {
            var itens = linhaTxt.Split('|');
            if (itens[0] == "H")
            {
                var cpf = itens[1] == "00000000000" ? "" : itens[1] == "00000000000000" ? "" : itens[1];
                if (!string.IsNullOrEmpty(cpf))
                {
                    if (cpf.Length < 14)
                    {
                        var isCpfValido = Zip.Utils.ValidaCpfCnpj.ValidaCpf(cpf);
                        if (!isCpfValido)
                        {
                            cpf = MessageBox.Show("O CPF informado é invalido\nDeseja informar novamente?", "Validação",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                      MessageBoxDefaultButton.Button1) ==
                                  DialogResult.Yes
                                ? cpf = FrmCpfCupom.Instance()
                                : "";
                        }
                    }
                    else
                    {
                        cpf = Utils.ValidaCpfCnpj.ValidarCNPJ(cpf) ? cpf : ""; 

                    }

                }


                _pedido.Cnpj = cpf;
                _pedido.Tipo = "V";
                _pedido.TipoPagamento = itens[2];
            }

            if (itens[0] == "D")
            {
                var pedidoItem = new VendaItemViewModel();

                pedidoItem.SeqProduto = int.Parse(itens[1]);
                var produtoId = int.Parse(itens[2]) > 0 ? int.Parse(itens[2]) : 999999;
                var produto = _produtoAppService.ObterPorId(produtoId);
                if (produto == null)
                    produto = _produtoAppService.ObterPorId(999999);

                pedidoItem.ProdutoId = produto.ProdutoId;
                pedidoItem.Produto = produto.Descricao.Trim();


                var quantidade = int.Parse(itens[3]);
                pedidoItem.Quantidade = quantidade / (decimal)100;
                var unitario = int.Parse(itens[4]);
                pedidoItem.ValorUnitatio = unitario / (decimal)100;


                _pedido.VendaItens.Add(pedidoItem);
            }

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Maximized;
                CarregaVendas();
            }
        }

        private void FormPrincipal_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                this.Hide();
        }

        private void lbEmpresa_Click(object sender, EventArgs e)
        {
            using (var form = new FormConfiguracao())
            {
                form.ShowDialog();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var nroSat = txtNroCupom.Text;
            if (string.IsNullOrEmpty(nroSat))
                return;

            var vendas = _vendaAppService.ObterNroSat(nroSat).OrderByDescending(t => t.VendaId);

            flowLayoutPanel1.Controls.Clear();
            //flowLayoutPanel2.Controls.Clear();
            foreach (var vendaViewModel in vendas)
            {
                var item = new UControlVenda(vendaViewModel);
                item.EnviarItem += Item_EnviarItem;
                item.CancelarItem += Item_CancelarItem;
                item.Width = flowLayoutPanel2.Width - 15;
                flowLayoutPanel1.Controls.Add(item);

            }
        }

        private void btnEnviarTodas_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.Controls.Count > 0)
            {
                timer1.Stop();
                try
                {
                    foreach (Control control in flowLayoutPanel2.Controls)
                    {
                        if (control.GetType() == typeof(UControlVenda))
                        {
                            var item = (UControlVenda)control;
                            var vendaView = item.VendaView;

                            if(!string.IsNullOrEmpty(vendaView.MenssagemSat))continue;

                            var isCpfValido = Zip.Utils.ValidaCpfCnpj.ValidaCpf(vendaView.Cnpj);
                            if (!isCpfValido)
                            {
                                vendaView.Cnpj = "";
                            }
                            vendaView.VendaFinalizadora = new List<CaixaPagamentoViewModel>()
                            {
                                new CaixaPagamentoViewModel()
                                {
                                    CodigoFiscal = vendaView.TipoPagamento == "DB" ? "04" : "03",
                                    Valor = vendaView.ValorTotal
                                }
                            };

                            using (var formSat = new FrmSolicitaSat(vendaView))
                            {
                                formSat.Visible = false;
                                formSat.ShowDialog();
                                

                                var result = formSat.RetornoSatView;
                                using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                                {
                                    retornoSatAppService.Adicionar(result);
                                }

                                if (result.IsOk)
                                    flowLayoutPanel2.Controls.Remove(control);
                            }
                        }
                    }

                    CarregaVendas();

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    timer1.Start();
                }
            }
        }

        private void RepassaVendas()
        {
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control.GetType() == typeof(UControlVenda))
                {
                    var item = (UControlVenda)control;
                    var vendaView = item.VendaView;


                    var isCpfValido = Zip.Utils.ValidaCpfCnpj.ValidaCpf(vendaView.Cnpj);
                    if (!isCpfValido)
                    {
                        vendaView.Cnpj = "";
                    }
                    vendaView.VendaFinalizadora = new List<CaixaPagamentoViewModel>()
                    {
                        new CaixaPagamentoViewModel()
                        {
                            CodigoFiscal = vendaView.TipoPagamento == "DB" ? "04" : "03",
                            Valor = vendaView.ValorTotal
                        }
                    };

                    using (var formSat = new FrmSolicitaSat(vendaView))
                    {
                        formSat.Visible = false;
                        formSat.ShowDialog();
                        

                        var result = formSat.RetornoSatView;
                        using (var retornoSatAppService = Program.Container.GetInstance<IRetornoSatAppService>())
                        {
                            retornoSatAppService.Adicionar(result);
                        }

                        if (result.IsOk)
                            flowLayoutPanel2.Controls.Remove(control);
                    }
                }
            }
        }
    }
}
