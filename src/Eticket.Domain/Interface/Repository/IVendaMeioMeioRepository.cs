using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IVendaMeioMeioRepository : IDisposable
    {
        void Add(VendaMeioMeio vendaMeioMeio);
        void Remove(VendaMeioMeio vendaMeioMeio);
        VendaMeioMeio GetById(int id);
        IEnumerable<VendaMeioMeio> GetByOperacaoId(int operacaoId, string operacaoTipo);
    }
}