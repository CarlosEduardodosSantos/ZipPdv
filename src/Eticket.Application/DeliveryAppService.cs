using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class DeliveryAppService : IDeliveryAppService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryAppService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public void Dispose()
        {
            _deliveryRepository.Dispose();
        }

        public void Entregar(DeliveryViewModel deliveryView)
        {
            var delivery = TypeAdapter.Adapt<DeliveryViewModel, Delivery>(deliveryView);
            _deliveryRepository.Entregar(delivery);
        }

        public void Retornar(DeliveryViewModel deliveryView)
        {
            var delivery = TypeAdapter.Adapt<DeliveryViewModel, Delivery>(deliveryView);
            _deliveryRepository.Retornar(delivery);
        }
    }
}