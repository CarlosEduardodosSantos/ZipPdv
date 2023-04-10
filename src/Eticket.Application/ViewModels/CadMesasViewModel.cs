using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class CadMesasViewModel
    {
        public int IdMesa { get; set; }
        public string Descricao { get; set; }
        public int IdPraca { get; set; }
        public bool Cob_Servico { get; set; }
        public int Status { get; set; }
        public int OpMesa1Atual { get; set; }
    }
}
