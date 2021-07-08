using Eticket.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.Interface
{
    public interface IProdutoOpcaoAppService
    {
        void Insert(ProdutoOpcaoViewModel produtoOpcao);
        void Relacionar(ProdutosOpcaoTipoRelacaoViewModel produtosOpcaoTipoRelacao);
        void Alterar(ProdutoOpcaoViewModel produtoOpcao);
        void Deletar(string id);
        void DeletarRelacao(string produtosOpcaoId, int produtoId);
        void InsertTipo(ProdutoOpcaoTipoVewModel produtoOpcaoTipo);
        void AlterarTipo(ProdutoOpcaoTipoVewModel produtoOpcaoTipo);
        IEnumerable<int> ObterProdutoOpcaoRelacao(int produtosOpcaoTipoId);

        IEnumerable<ProdutoOpcaoTipoVewModel> GetByprodutoId(int produtoId);
        IEnumerable<ProdutoOpcaoTipoVewModel> ObterTipoAll();
    }
}
