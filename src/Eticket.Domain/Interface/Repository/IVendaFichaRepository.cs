using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IVendaFichaRepository : IDisposable
    {
        void Add(VendaFicha vendaFicha);
        void Remover(VendaFicha vendaFicha);
        IEnumerable<VendaFicha> ObterPorFicha(string ficha);
        int ObterUltimaSequencia(string ficha);
        void ImprimeFichaGr(string ficha);
    }
}