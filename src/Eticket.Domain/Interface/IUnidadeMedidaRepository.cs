using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Domain.Interface
{
    public interface IUnidadeMedidaRepository : IRepositoryBase<UnidadeMedida>
    {
        void Adicionar(UnidadeMedida unidade);
        void Alterar(UnidadeMedida unidade);
        void Remover(UnidadeMedida unidade);
    }
}