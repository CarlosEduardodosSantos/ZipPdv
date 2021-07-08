using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Application.ViewModels
{
    public class ProdutoOpcaoTipoVewModel
    {
        public int ProdutosOpcaoTipoId { get; set; }
        public int RestauranteId { get; set; }
        public int Situacao { get; set; }
        public int Tipo { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int QtdeMax { get; set; }
        public bool Obrigatorio { get; set; }
        public int Sequencia { get; set; }
        public List<ProdutoOpcaoViewModel> ProdutoOpcaos { get; set; }
    }
}
