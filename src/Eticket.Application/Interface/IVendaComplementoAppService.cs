using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IVendaComplementoAppService : IDisposable
    {
        void Add(VendaComplementoViewModel vendaComplementoView);
        void Remove(VendaComplementoViewModel vendaComplementoView);
        VendaComplementoViewModel GetById(int id);
        IEnumerable<VendaComplementoViewModel> ObterPorFicha(string ficha);
        IEnumerable<VendaComplementoViewModel> ObterPorPendenciaId(int pendenciaId);
        IEnumerable<VendaComplementoViewModel> ObterPorMesaId(int mesaId);
        IEnumerable<VendaComplementoViewModel> ObterPorVendaId(int vendaId);
    }
}