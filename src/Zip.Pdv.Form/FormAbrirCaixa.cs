using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Zip.Pdv.Component;

namespace Zip.Pdv
{
    public partial class FormAbrirCaixa : Form
    {
        private static bool _caixaAberto;
        public FormAbrirCaixa()
        {
            InitializeComponent();
            txtpdv.Text = Program.Pdv.ToString();
            txtUsuario.Text = Program.Usuario.Nome;
            txtValorInicial.Select();
        }
        public static bool AbrirCaixa()
        {
            using (var form = new FormAbrirCaixa())
            {
                form.ShowDialog();
                return _caixaAberto;
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            var caixa = new CaixaViewModel()
            {
                CedenteId = Program.InicializacaoViewAux.CedenteId,
                DataInicio = DateTime.Now,
                Loja = Program.Loja,
                Pdv = Program.Pdv,
                ValorAbertura = txtValorInicial.ValueNumeric
            };

            using (var caixaApp = Program.Container.GetInstance<ICaixaAppService>())
            {
                caixaApp.Abrir(caixa);

            }


            using (var caixaApp = Program.Container.GetInstance<ICaixaItemAppService>())
            {
                var caixaItem = new CaixaItemViewModel()
                {
                    CaixaId = Program.CaixaView.CaixaId,
                    
                    UsuarioId = Program.Usuario.UsuarioId,
                    DataHora = DateTime.Now,
                    Valor = txtValorInicial.ValueNumeric,
                    TipoLancamento = "INI",
                    Historico = $"TROCO INICIAL"
                };

                using (var especieAppService = Program.Container.GetInstance<IEspeciePagamentoAppService>())
                {
                    var especie = especieAppService.ObterTodos().FirstOrDefault(t => t.Interno == "ESP1");

                    caixaItem.CaixaPagamentos.Add(new CaixaPagamentoViewModel()
                    {
                        Especie = especie.Especie,
                        Valor = caixaItem.Valor,
                        EspeciePagamentoId = especie.EspeciePagamentoId,
                        Interno = especie.Interno,
                        CodigoFiscal = especie.CodigoFiscal
                    });

                    caixaApp.Adicionar(caixaItem);
                }
            }
            if (Program.InicializacaoViewAux.HabZerarSenha)
            {
                var result = TouchMessageBox.Show("Deseja zerar sequência de senhas?", "Zerar senha",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    using (var configuracaoAppService = Program.Container.GetInstance<IConfiguracaoSistemaAppService>())
                    {
                        configuracaoAppService.ZerarSenha();
                    }
                }

            }

            _caixaAberto = true;

            Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            _caixaAberto = false;
            Close();
        }

        private void keyboardNum1_UserKeyPressed(object sender, KeyboardClassLibrary.Num.KeyboardNumEventArgs e)
        {

            if (e.KeyboardKeyPressed == "{ENTER}")
            {

                btnAbrir.PerformClick();
            }
            else
            {
                if (!string.IsNullOrEmpty(e.KeyboardKeyPressed))
                {
                    if (txtValorInicial.SelectionLength > 0)
                        txtValorInicial.Clear();

                    var str = new StringBuilder();
                    //Verifica se é comando para apagar
                    if (e.KeyboardKeyPressed == "{BACKSPACE}")
                    {
                        str.Append(int.Parse(txtValorInicial.ValueNumeric.ToString().Replace(",", "")));
                        str.Remove(str.Length - 1, 1);
                    }
                    else
                    {
                        str.Append(int.Parse(txtValorInicial.ValueNumeric.ToString().Replace(",", "")));
                        str.Append(e.KeyboardKeyPressed);
                    }

                    txtValorInicial.ValueNumeric = str.Length > 0 ? decimal.Parse(str.ToString()) / 100 : 0;
                }

            }
            txtValorInicial.SelectionStart = txtValorInicial.Text.Length;
        }

        private void FormAbrirCaixa_Load(object sender, EventArgs e)
        {
            using (var caixaApp = Program.Container.GetInstance<ICaixaAppService>())
            {
                var caixaAnt = caixaApp.ObterUltimoCaixaFechado(Program.Loja, Program.Pdv);

                if (caixaAnt.ValorFechamento > 0)
                {
                    txtValorInicial.ValueNumeric = caixaAnt.ValorFechamento;
                    txtValorInicial.ReadOnly = true; 
                }
                

            }
        }
    }
}
