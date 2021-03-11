using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoComplementoRepository : IDisposable
    {
        ProdutoComplemento GetById(int id);
        IEnumerable<ProdutoComplemento> ObterPorGrupoId(int grupoId);
    }
}