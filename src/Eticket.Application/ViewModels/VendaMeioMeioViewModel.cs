using System;

namespace Eticket.Application.ViewModels
{
    public class VendaMeioMeioViewModel
    {
        public int VendaMeioMeioId { get; set; }
        public int OperacaoId { get; set; }
        public string OperacaoTipo { get; set; }
        public int ProdutoId { get; set; }
        public int Seguencia { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public string QtdeImpresso { get; set; }
         
    }
}