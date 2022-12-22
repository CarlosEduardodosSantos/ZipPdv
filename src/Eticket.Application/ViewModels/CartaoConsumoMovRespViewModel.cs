using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class CartaoConsumoMovRespViewModel
    {
        public Guid MovId { get; set; }
        public bool Aproved { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public string Mensage { get; set; }
        public bool Frete { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}
