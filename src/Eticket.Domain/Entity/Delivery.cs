using System;

namespace Eticket.Domain.Entity
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int VendaId { get; set; }
        public int ClienteDeliveryId { get; set; }
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
        public ClienteDelivery ClienteDelivery { get; set; }
    }
}