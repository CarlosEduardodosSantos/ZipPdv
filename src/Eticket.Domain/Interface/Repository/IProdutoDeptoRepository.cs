using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IProdutoDeptoRepository : IRepositoryBase<ProdutoDepto>
    {
        void Adicionar(ProdutoDepto produtoSecao);
        void Editar(ProdutoDepto produtoSecao);
        void Excluir(ProdutoDepto produtoSecao);
    }
}