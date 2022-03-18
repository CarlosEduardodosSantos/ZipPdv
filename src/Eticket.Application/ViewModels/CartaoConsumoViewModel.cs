using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class CartaoConsumoViewModel
    {
        public Guid CartaoConsumoId { get; set; }

        public string Numero { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public string Validade { get; set; }

        public string Cpf { get; set; }

        public decimal Desconto { get; set; }

        public string Nome { get; set; }
        public int RestauranteId { get; set; }

        public decimal SaldoAtual { get; set; }

    }
}
