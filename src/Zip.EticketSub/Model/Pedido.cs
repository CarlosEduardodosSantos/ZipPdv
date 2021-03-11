using System;
using System.Collections.Generic;
using System.Linq;

namespace Zip.EticketSub.Model
{
    public class Pedido
    {
        public Pedido()
        {
            PedidoItems = new List<PedidoItem>();
            DataHora = DateTime.Now;
        }
        public int PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public string CartaoId { get; set; }
        public string Cpf { get; set; }
        public string TipoPagamento { get; set; }
        public DateTime DataHora { get; set; }
        public decimal ValorPedido => PedidoItems.Sum(t => t.Unitario * t.Quantidade);
        public ICollection<PedidoItem> PedidoItems { get; set; }
    }

    public class PedidoItem
    {
        public int PedidoItemId { get; set; }
        public int PedidoId { get; set; }
        public int Sequencia { get; set; }
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Unitario { get; set; }
        public decimal Total => (Unitario * Quantidade);
    }
}