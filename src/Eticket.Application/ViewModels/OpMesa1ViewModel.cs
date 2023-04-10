using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class OpMesa1ViewModel
    {
        public int? IdopMesa1 { get; set; }
        public int? IdGarcom { get; set; }
        public int? IdMesa { get; set; }
        public DateTime? dthrInicial { get; set; }
        public string Status { get; set; }
        public int? QtdePessoas { get; set; }
        public decimal? Dinheiro { get; set; }
        public decimal? Cheque { get; set; }
        public decimal? Cartao_Debito { get; set; }
        public decimal? Cartao_Credito { get; set; }
        public decimal ? Cartao_Consumo { get; set; }
        public decimal? Ticket { get; set; }
        public decimal? Troco { get; set; }
        public int? Venda_Nro { get; set; }
    }
}
