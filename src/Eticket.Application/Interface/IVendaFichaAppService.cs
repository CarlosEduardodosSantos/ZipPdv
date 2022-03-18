using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IVendaFichaAppService : IDisposable
    {
        void Add(IEnumerable<VendaFichaViewModel> vendaFichaView);
        void Remover(VendaFichaViewModel vendaFichaView);
        IEnumerable<VendaFichaViewModel> ObterPorFicha(int[] ficha);
        bool FicheExiste(int fichaId);
        VendaFichaViewModel ObterFichaByGuid(string fichaGuid);
        int ObterUltimaSequencia(string ficha);
        void ImprimeFichaGr(string ficha);
        void FinalizaFicha(string fichaId);
    }
}