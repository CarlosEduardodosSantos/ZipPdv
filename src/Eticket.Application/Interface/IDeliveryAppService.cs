using System;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IDeliveryAppService : IDisposable
    {
        void Entregar(DeliveryViewModel deliveryView);
        void Retornar(DeliveryViewModel deliveryView);
    }
}