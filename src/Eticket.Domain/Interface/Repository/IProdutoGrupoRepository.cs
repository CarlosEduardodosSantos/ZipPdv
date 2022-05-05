using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoGrupoRepository : IDisposable
    { 
        ProdutoGrupo GetById(int id);
        IEnumerable<ProdutoGrupo> GetAll(int loja);
    }
}