using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoSecaoRepository : IRepositoryBase<ProdutoSecao>
    {
        void Adicionar(ProdutoSecao produtoSecao);
        void Editar(ProdutoSecao produtoSecao);
        void Excluir(ProdutoSecao produtoSecao);
    }
}