using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class ProdutoPromocaoViewModel
    {
        public int IdPromocao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
