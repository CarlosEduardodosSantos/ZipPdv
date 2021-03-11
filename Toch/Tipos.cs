using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Toch
{
    public enum TipoProduto
    {
        [Description("Produto")]
        P = 0,
        [Description("Composto")]
        C = 1,
        [Description("Meio/Meio")]
        M = 2,
        [Description("Materia Prima")]
        T = 3
    };

}
