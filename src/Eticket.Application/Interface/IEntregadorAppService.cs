using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IEntregadorAppService : IDisposable
    {
        EntregadorViewModel ObterPorId(int entregadorId);
        IEnumerable<EntregadorViewModel> ObterTodos();
    }
}