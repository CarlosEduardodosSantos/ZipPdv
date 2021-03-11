using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoTipoAppService : IDisposable
    {
        ProdutoTipoViewModel ObterPorId(int id);
        IEnumerable<ProdutoTipoViewModel> ObterTodos();
    }
}