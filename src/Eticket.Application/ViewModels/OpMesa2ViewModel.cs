using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class OpMesa2ViewModel
    {
        public int  idOpMesa2 { get; set; }
        public int  idOpMesa1 { get; set; }
        public int  CodProduto { get; set; }
        public decimal  Qtde { get; set; }
        public bool  Status { get; set; }
        public decimal  Valor { get; set; }
        public string DesProduto { get; set; }
        public decimal  Desconto { get; set; }
        public decimal  VlUnit { get; set; }
        public string Obs { get; set; }
        public bool Pago { get; set; }
        public string Metodo { get; set; }
    }
}
