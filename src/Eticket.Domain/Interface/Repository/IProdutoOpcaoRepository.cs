using Eticket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoOpcaoRepository
    {
        void Insert(ProdutoOpcao produtoOpcao);
        void Relacionar(ProdutosOpcaoTipoRelacao produtosOpcaoTipoRelacao);
        void Alterar(ProdutoOpcao produtoOpcao);
        void Deletar(string id);
        void DeletarRelacao(string produtosOpcaoId, int produtoId);
        void InsertTipo(ProdutoOpcaoTipo produtoOpcaoTipo);
        void AlterarTipo(ProdutoOpcaoTipo produtoOpcaoTipo);
        IEnumerable<int> ObterProdutoOpcaoRelacao(int produtosOpcaoTipoId);

        IEnumerable<ProdutoOpcaoTipo> GetByprodutoId(int produtoId);
        IEnumerable<ProdutoOpcaoTipo> ObterTipoAll();

    }
}
