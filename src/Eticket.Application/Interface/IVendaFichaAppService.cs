using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IVendaFichaAppService : IDisposable
    {
        void Add(IEnumerable<VendaFichaViewModel> vendaFichaView);
        void Remover(VendaFichaViewModel vendaFichaView);
        IEnumerable<VendaFichaViewModel> ObterPorFicha(string ficha);
        int ObterUltimaSequencia(string ficha);
        void ImprimeFichaGr(string ficha);
    }
}