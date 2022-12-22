using System;

namespace Eticket.Domain.Entity
{
    public class VendaProdutoOpcao
    {
        public int VendaProdutoOpcaoId { get; set; }
        public Guid ProdutosOpcaoId { get; set; }
        public int ProdutosOpcaoTipoId { get; set; }
        public string Descricao { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoPdv { get; set; }
        public decimal Valor { get; set; }
        public string Ficha { get; set; }
        public int MesaOperacaoId { get; set; }
        public int PendenteId { get; set; }
        public int VendaId { get; set; }
        public int Sequencia { get; set; }
        public decimal Quantidade { get; set; }
    }
}
