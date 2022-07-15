using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class VendaPendenteViewModel
    {
        public int Chave { get; set; }
        public int Nro { get; set; }
        public DateTime DataHora { get; set; }
        public string Hora { get; set; }
        public int ProdutoId { get; set; }
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Desconto { get; set; }
        public decimal Unitario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public int UsuarioId { get; set; }
        public decimal ValorVenda { get; set; }
        public string Estacao { get; set; }
        public string Observacao { get; set; }
        public int SeqProduto { get; set; }
        public string Cliente { get; set; }
        public string Senha { get; set; }

        public IEnumerable<VendaPendenteViewModel> ConvertVendaToVendaPendencia(VendaViewModel venda)
        {
            var vendaPendente = new List<VendaPendenteViewModel>();

            foreach (var item in venda.VendaItens)
            {
                vendaPendente.Add(new VendaPendenteViewModel()
                {
                    Nro = venda.PendenciaId,
                    DataHora = venda.DataHora.Date,
                    Hora = venda.HoraPendencia ?? venda.DataHora.ToShortTimeString(),
                    Cliente = venda.ClientePendencia,
                    Produto = item.DescricaoProduto,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    Unitario = item.ValorUnitatio,
                    Total = item.ValorTotal,
                    SubTotal = item.SubTotal,
                    Observacao = item.Observacao,
                    SeqProduto = item.SeqProduto,
                    ValorVenda = item.ValorUnitatio,
                    UsuarioId = venda.UsuarioId
                });
            }


            return vendaPendente;
        }
        public List<VendaViewModel> ConvertVendaPendenciaToVenda(IEnumerable<VendaPendenteViewModel> vendaPendentes)
        {
            var vendas = new List<VendaViewModel>();

            

            return vendas;
        }
    }
}
