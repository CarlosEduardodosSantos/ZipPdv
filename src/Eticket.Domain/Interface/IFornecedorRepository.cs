using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IFornecedorRepository : IRepositoryBase<Fornecedor>
    {
        void Adicionar(Fornecedor fornecedor);
        void Editar(Fornecedor fornecedor);
        void Excluir(Fornecedor fornecedor);
    }
}