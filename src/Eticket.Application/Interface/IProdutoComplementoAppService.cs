using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoComplementoAppService : IDisposable
    {
        ProdutoComplementoViewModel GetById(int id);
        IEnumerable<ProdutoComplementoViewModel> ObterPorGrupoId(int grupoId);
    }
}