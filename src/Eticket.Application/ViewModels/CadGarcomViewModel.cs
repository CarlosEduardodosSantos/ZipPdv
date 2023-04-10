using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class CadGarcomViewModel
    {
        public int IdGarcom { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }

        public string Senha { get; set; }

        public bool Adm { get; set; }
    }
}
