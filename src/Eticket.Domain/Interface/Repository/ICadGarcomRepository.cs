using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICadGarcomRepository: IRepositoryBase<CadGarcom>
    {
        CadGarcom GetById(int id);

        IEnumerable<CadGarcom> ObterGarcons();
    }
}
