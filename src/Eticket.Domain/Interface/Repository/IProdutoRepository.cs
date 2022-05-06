using System;
using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoRepository : IDisposable
    {
        Produto GetById(int id);
        IEnumerable<Produto> GetAll();
        IEnumerable<Produto> GetByGrupoId(int grupoId);
        IEnumerable<Produto> GetByMeioMeio();
        IEnumerable<Produto> ObterPorEan(string ean);
        IEnumerable<Produto> ObterPorNome(string nome);
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