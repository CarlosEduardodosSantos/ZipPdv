using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface ICaixaItemAppService : IDisposable
    {
        void Adicionar(CaixaItemViewModel caixaItemView);
        IEnumerable<CaixaItemViewModel> ObterPorCaixaId(int caixaId);
    }
}