using System;

namespace Eticket.Application.ViewModels
{
    public class DeliveryViewModel
    {
        public DeliveryViewModel()
        {
            ClienteDelivery = new ClienteDeliveryViewModel();
            DataHora = DateTime.Now;
        }
        public int DeliveryId { get; set; }
        public int VendaId { get; set; }
        public int ClienteDeliveryId => ClienteDelivery.ClienteDeliveryId;
        public DateTime DataHora { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public DateTime DataHoraRetorno { get; set; }
        public int UsuarioSaida { get; set; }
        public int UsuarioRetorno { get; set; }
        public decimal Troco { get; set; }
        public decimal TaxaEntrega { get; set; }
        public decimal Valor { get; set; }
        public int EmpresaId { get; set; }
        public int EntregadorId { get; set; }
        public string Entregador { get; set; }
        public ClienteDeliveryViewModel ClienteDelivery { get; set; }
    }
}