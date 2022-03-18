using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class CartaoConsumoMov
    {
        public int RestauranteId { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataMov { get; set; }
        public decimal Valor { get; set; }
        public int TipoMov { get; set; }
        public string Historico { get; set; }
    }
}
