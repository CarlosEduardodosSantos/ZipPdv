using System;

namespace Eticket.Domain.Entity.DashBoard
{
    public class DashBoardVenda
    {
        public int VendaId { get; set; }
        public DateTime DataHora { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal CustoTotal { get; set; }
        public decimal Desconto { get; set; }
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
    }
}