using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IDeliveryRepository : IRepositoryBase<Delivery>
    {
        void Entregar(Delivery delivery);
        void Retornar(Delivery delivery);
    }
}