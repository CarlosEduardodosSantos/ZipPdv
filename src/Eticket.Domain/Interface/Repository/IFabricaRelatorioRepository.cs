using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IFabricaRelatorioRepository : IRepositoryBase<FabricaRelatorio>
    {
        void Adcionar(FabricaRelatorio fabricaRelatorio);
        void Editar(FabricaRelatorio fabricaRelatorio);
        void Remover(FabricaRelatorio fabricaRelatorio);
        FabricaRelatorio GetByGuid(Guid id);
        IEnumerable<FabricaRelatorio> ObterPorPesquisa(string pesquisa);
    }
}