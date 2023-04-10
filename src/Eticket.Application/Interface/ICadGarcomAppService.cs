using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.Interface
{
    public interface ICadGarcomAppService: IDisposable
    {
        CadGarcomViewModel GetById(int id);

        IEnumerable<CadGarcomViewModel> ObterGarcons();
    }
}
