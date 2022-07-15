using System;

namespace Eticket.Domain.Entity
{
    public class VendaPendente
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

    }
}
