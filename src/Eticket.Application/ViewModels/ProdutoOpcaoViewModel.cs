using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class ProdutoOpcaoViewModel
    {
        public ProdutoOpcaoViewModel()
        {
            ProdutosOpcaoId = Guid.NewGuid();
        }
        public Guid ProdutosOpcaoId { get; set; }
        public int ProdutosOpcaoTipoId { get; set; }
        public int Situacao { get; set; }
        public int RestauranteId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Sequencia { get; set; }
        public string ProdutoPdv { get; set; }
        public bool Replicar { get; set; }
    }
}
