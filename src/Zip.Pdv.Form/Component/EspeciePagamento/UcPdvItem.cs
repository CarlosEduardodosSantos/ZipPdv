using System;
using System.Windows.Forms;
using Eticket.Application.ViewModels;

namespace Zip.Pdv.Component.EspeciePagamento
{
    public partial class UcPdvItem : UserControl
    {
        public event EventHandler<EventArgs> Click;
        public object CaixaSource { get; set; }
        void cClick(object sender, EventArgs e)
        {
            var completedEvent = Click;
            if (completedEvent != null)
            {
                var item = (UcPdvItem)this;
                completedEvent(item, e);
            }
        }
        public UcPdvItem()
        {
            InitializeComponent();
        }

        public void AdicionarCaixaPagamento(CaixaPagamentoViewModel caixaPagamento)
        {
            CaixaSource = caixaPagamento;
            lbDisplay.Text = caixaPagamento.Especie;
            lbValue.Text = caixaPagamento.Valor.ToString("C2");
            btnDeletar.Click += cClick;
        }
        public void AdicionarCaixaFechamento(CaixaFechamentoViewModel caixaFechamento)
        {
            CaixaSource = caixaFechamento;
            lbDisplay.Text = caixaFechamento.Especie;
            lbValue.Text = caixaFechamento.Valor.ToString("C2");
            btnDeletar.Click += cClick;
        }
        public void AdicionarComplemento(VendaComplementoViewModel vendaComplemento)
        {
            CaixaSource = vendaComplemento;
            lbDisplay.Text = vendaComplemento.Descricao;
            lbValue.Text = vendaComplemento.Valor.ToString("C2");
            btnDeletar.Click += cClick;
        }
        public void AdicionarComplemento(VendaItemViewModel vendaItem)
        {
            CaixaSource = vendaItem;
            lbDisplay.Text = vendaItem.DescricaoProduto;
            lbValue.Text = vendaItem.ValorUnitatio.ToString("C2");
            btnDeletar.Click += cClick;
        }

        public void AdicionarProdutoOpcoes(VendaProdutoOpcaoViewModel vendaProdutoOpcaoView)
        {
            CaixaSource = vendaProdutoOpcaoView;
            if (vendaProdutoOpcaoView.Quantidade > 0)
                lbDisplay.Text = $"{vendaProdutoOpcaoView.Quantidade} x {vendaProdutoOpcaoView.Descricao}";
            else
                lbDisplay.Text = $"{vendaProdutoOpcaoView.Descricao}";

            lbValue.Text = $"{vendaProdutoOpcaoView.Valor.ToString("C2")}";
            btnDeletar.Click += cClick;
        }
        public void AdicionarFicha(int ficha)
        {
            this.Width = 240;
            CaixaSource = ficha;
            lbDisplay.Text = $"FICHA: {ficha}";
            lbValue.Visible = false;
            btnDeletar.Click += cClick;
        }
    }
}
