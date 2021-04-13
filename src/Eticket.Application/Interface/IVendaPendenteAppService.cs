using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Eticket.Application.Interface
{
    public interface IVendaPendenteAppService : IDisposable
    {
        void Add(VendaViewModel venda);
        void Remover(VendaViewModel venda);
        IEnumerable<VendaViewModel> ObterPorNome(string nome);
        int ObterUltimaSequencia(string nome);
        void ImprimePendencia(string pendenciaId);
        bool PendenciaExistente(string nome);
    }
}
