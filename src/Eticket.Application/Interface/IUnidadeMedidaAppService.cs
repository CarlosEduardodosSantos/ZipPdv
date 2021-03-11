using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IUnidadeMedidaAppService : IDisposable
    {
        UnidadeMedidaViewModel ObterPorId(int id);
        IEnumerable<UnidadeMedidaViewModel> ObterTodos();
        void Adicionar(UnidadeMedidaViewModel unidadeView);
        void Alterar(UnidadeMedidaViewModel unidadeView);
        void Remover(UnidadeMedidaViewModel unidadeView);
    }
}