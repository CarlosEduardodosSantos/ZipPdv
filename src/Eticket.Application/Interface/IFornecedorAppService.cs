using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IFornecedorAppService : IDisposable
    {
        void Adicionar(FornecedorViewModel fornecedorView);
        void Editar(FornecedorViewModel fornecedorView);
        void Excluir(FornecedorViewModel fornecedorView);
        FornecedorViewModel ObterPorId(int id);
        IEnumerable<FornecedorViewModel> ObterTodos();
    }
}