using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IEspeciePagamentoAppService : IDisposable
    {
        EspeciePagamentoViewModel ObterPorId(int id);
        IEnumerable<EspeciePagamentoViewModel> ObterTodos();
    }
}