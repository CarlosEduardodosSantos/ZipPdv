using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class ClienteDeliveryAppService : IClienteDeliveryAppService
    {
        private readonly IClienteDeliveryRepository _clienteDeliveryRepository;

        public ClienteDeliveryAppService(IClienteDeliveryRepository clienteDeliveryRepository)
        {
            _clienteDeliveryRepository = clienteDeliveryRepository;
        }

        public void Dispose()
        {
            _clienteDeliveryRepository.Dispose();
        }

        public ClienteDeliveryViewModel ObterPorFone(string fone)
        {
            return TypeAdapter.Adapt<ClienteDelivery, ClienteDeliveryViewModel>(
                _clienteDeliveryRepository.ObterPorFone(fone));
        }

        public decimal TaxaPorBairro(string bairro)
        {
            return _clienteDeliveryRepository.TaxaPorBairro(bairro);
        }
    }
}