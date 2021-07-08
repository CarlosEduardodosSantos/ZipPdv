using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoSecaoAppService : IDisposable
    {
        void Adicionar(ProdutoSecaoViewModel produtoSecaoView);
        void Editar(ProdutoSecaoViewModel produtoSecaoView);
        void Excluir(ProdutoSecaoViewModel produtoSecaoView);
        IEnumerable<ProdutoSecaoViewModel> ObterTodos();
    }
}