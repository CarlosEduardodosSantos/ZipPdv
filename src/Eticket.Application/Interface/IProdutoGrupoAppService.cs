using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoGrupoAppService : IDisposable
    {
        ProdutoGrupoViewModel ObterPorId(int id);
        IEnumerable<ProdutoGrupoViewModel> ObterTodos(int lojaId);
    }
}