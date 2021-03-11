using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IFichaAppService : IDisposable
    {
        FichaViewModel GetByFichaId(string fichaId);
        IEnumerable<FichaViewModel> GetAll();
    }
}