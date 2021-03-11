using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IFabricaRelatorioAppService : IDisposable
    {
        void Adcionar(FabricaRelatorioViewModel fabricaRelatorioViewModel);
        void Editar(FabricaRelatorioViewModel fabricaRelatorioViewModel);
        void Remover(FabricaRelatorioViewModel fabricaRelatorioViewModel);
        FabricaRelatorioViewModel ObterPorId(Guid id);
        IEnumerable<FabricaRelatorioViewModel> ObterTodos();
        IEnumerable<FabricaRelatorioViewModel> ObterPorPesquisa(string pesquisa);
    }
}