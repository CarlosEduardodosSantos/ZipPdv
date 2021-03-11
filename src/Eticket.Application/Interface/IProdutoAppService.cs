using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoAppService : IDisposable
    {
        ProdutoViewModel ObterPorId(int id);
        IEnumerable<ProdutoViewModel> ObterPorGrupoId(int grupoId);
        IEnumerable<ProdutoViewModel> ObterMeioMeio();
        IEnumerable<ProdutoViewModel> ObterPorEan(string ean);
        IEnumerable<ProdutoViewModel> ObterPorNome(string nome);
        string ObterImageProdutoId(int produtoId);
        ProdutoTributacaoViewModel ObterTributacaoPorProdutoId(int produtoId);
    }
}