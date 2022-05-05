using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoRepository : IDisposable
    {
        Produto GetById(int id);
        IEnumerable<Produto> GetAll(int loja);
        IEnumerable<Produto> GetByGrupoId(int loja, int grupoId);
        IEnumerable<Produto> GetByMeioMeio();
        IEnumerable<Produto> ObterPorEan(int loja, string ean);
        IEnumerable<Produto> ObterPorNome(int loja, string nome);
        IEnumerable<Produto> ObterMaisVendidos();
        IEnumerable<Produto> ObterAbaixoDoMinimo();
        IEnumerable<Produto> ObterEmFalta();
        IEnumerable<Produto> ObterEmExesso();
        string ObterImageProdutoId(int produtoId);
        ProdutoTributacao ObterTributacaoPorProdutoId(int produtoId);
        IEnumerable<ProdutoObservacao> ObterProdutoObservacao(int grupoId);
        IEnumerable<Produto> GetSugestaoByGrupoId(int grupoId);
    }
}