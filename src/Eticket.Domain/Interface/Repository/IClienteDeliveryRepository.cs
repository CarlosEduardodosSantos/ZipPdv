using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface IClienteDeliveryRepository : IRepositoryBase<ClienteDelivery>
    {
        ClienteDelivery ObterPorFone(string fone);
        decimal TaxaPorBairro(string bairro);
    }
}