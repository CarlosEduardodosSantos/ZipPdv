using System;
using System.Collections.Generic;

namespace Eticket.Domain.Entity
{
    public class Caixa
    {
        public int CaixaId { get; set; }
        public int Loja { get; set; }
        public int Pdv { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public int UsuarioFinal { get; set; }
        public DateTime DataFinal { get; set; }
        public decimal ValorAbertura { get; set; }
        public int  CedenteId { get; set; }
        public ICollection<CaixaFechamento> CaixaFechamentos { get; set; }
    }
}