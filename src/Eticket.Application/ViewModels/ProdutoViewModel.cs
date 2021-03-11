using System.Collections.Generic;
using System.Windows.Controls;

namespace Eticket.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }
        public string ProdutoTipo { get; set; }
        public int GrupoId { get; set; }
        public string Unidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCusto { get; set; }
        public string Imagem { get; set; }
        public bool ParaBalanca { get; set; }
        public ICollection<ProdutoComplementoViewModel> ProdutoComplementos { get; set; }
    }
}