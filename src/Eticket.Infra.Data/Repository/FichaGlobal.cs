using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Infra.Data.Repository
{
    public class FichaGlobal
    {
        private static bool fichatela = false;
        public static bool telaficha { 
            get { return fichatela; }  
            set { fichatela = value; }
        }
    }
}
