using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface ICaixaFechamentoAppService : IDisposable
    {
        IEnumerable<CaixaFechamentoViewModel> ObterPorCaixaId(int caixaId);
    }
}