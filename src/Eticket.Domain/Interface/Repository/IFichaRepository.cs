using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IFichaRepository : IDisposable
    {
        Ficha GetByFichaId(string fichaId);
        IEnumerable<Ficha> GetAll();
    }
}