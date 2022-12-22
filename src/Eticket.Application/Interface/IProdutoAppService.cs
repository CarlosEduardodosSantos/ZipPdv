using System;
using System.Collections.Generic;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IProdutoAppService : IDisposable
    {
        ProdutoViewModel ObterPorId(int id);
        IEnumerable<ProdutoViewModel> ObterPorGrupoId(int grupoId, int lojaId);
        IEnumerable<ProdutoViewModel> ObterMeioMeio();
        IEnumerable<ProdutoViewModel> ObterPorEan(string ean);
        IEnumerable<ProdutoViewModel> ObterPorNome(string nome);
        IEnumerable<ProdutoViewModel> ObterMaisVendidos();
        IEnumerable<ProdutoViewModel> ObterAbaixoDoMinimo();
        IEnumerable<ProdutoViewModel> ObterEmFalta();
        IEnumerable<ProdutoViewModel> ObterEmExesso();
        string ObterImageProdutoId(int produtoId);
        ProdutoTributacaoViewModel ObterTributacaoPorProdutoId(int produtoId);
        IEnumerable<ProdutoObservacaoViewModel>  ObterProdutoObservacao(int grupoId);
        IEnumerable<ProdutoViewModel> GetSugestaoByGrupoId(int grupoId);
        IEnumerable<ProdutoPromocaoViewModel> ObterProdutoPromocao(int produtoId);
    }
}