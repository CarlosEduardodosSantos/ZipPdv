using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoDeptoAppService : IDisposable
    {
        void Adicionar(ProdutoDeptoViewModel produtoDeptoView);
        void Editar(ProdutoDeptoViewModel produtoDeptoView);
        void Excluir(ProdutoDeptoViewModel produtoDeptoView);
        IEnumerable<ProdutoDeptoViewModel> ObterTodos();
    }
}