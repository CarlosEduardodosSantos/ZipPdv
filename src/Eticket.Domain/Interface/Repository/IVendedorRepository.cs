using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IVendedorRepository : IDisposable
    {
        Vendedor GetById(int id);
        IEnumerable<Vendedor> GetAll();
        Vendedor GetAutenticacao(string senha);
    }
}