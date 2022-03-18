using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class CartaoConsumoMovRespViewModel
    {
        public bool Aproved { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public string Mensage { get; set; }
    }
}
