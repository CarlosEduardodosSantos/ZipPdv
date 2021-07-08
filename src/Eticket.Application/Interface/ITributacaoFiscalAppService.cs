using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface ITributacaoFiscalAppService : IDisposable
    {
        void Adicionar(ProdutoTributacaoViewModel tributacaoFiscalView);
        void Editar(ProdutoTributacaoViewModel tributacaoFiscalView);
        void Excluir(ProdutoTributacaoViewModel tributacaoFiscalView);
        IEnumerable<ProdutoTributacaoViewModel> ObterTodos();
    }
}