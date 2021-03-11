using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IVendaComplementoRepository : IDisposable
    {
        void Add(VendaComplemento vendaComplemento);
        void Remove(VendaComplemento vendaComplemento);
        VendaComplemento GetById(int id);
        IEnumerable<VendaComplemento> ObterPorFicha(string ficha);
        IEnumerable<VendaComplemento> ObterPorPendenciaId(int pendenciaId);
        IEnumerable<VendaComplemento> ObterPorMesaId(int mesaId);
        IEnumerable<VendaComplemento> ObterPorVendaId(int vendaId);
    }
}