using System;

namespace Eticket.Domain.Entity
{
    public class VendaFicha
    {
        public int FichaItemId { get; set; }
        public string Ficha { get; set; }
        public int Loja { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitatio { get; set; }
        public decimal ValorTotal { get; set; }
        public int VendedorId { get; set; }
        public int Sequencia { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public int ClienteFichaId { get; set; }

    }
}