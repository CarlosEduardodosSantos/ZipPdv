using System;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface IClienteDeliveryAppService : IDisposable
    {
        ClienteDeliveryViewModel ObterPorFone(string fone);
    }
}