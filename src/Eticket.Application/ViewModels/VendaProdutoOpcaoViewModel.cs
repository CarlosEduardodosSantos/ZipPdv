using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class VendaProdutoOpcaoViewModel
    {
        public int VendaProdutoOpcaoId { get; set; }
        public Guid ProdutosOpcaoId { get; set; }
        public int ProdutosOpcaoTipoId { get; set; }
        public string Descricao { get; set; }
        public int ProdutoId { get; set; }
        public decimal Valor { get; set; }
        public string Ficha { get; set; }
        public int MesaOperacaoId { get; set; }
        public int PendenteId { get; set; }
        public int VendaId { get; set; }
        public int Sequencia { get; set; }
    }
}
