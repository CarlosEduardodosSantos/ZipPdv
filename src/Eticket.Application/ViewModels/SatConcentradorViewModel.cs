using System;

namespace Eticket.Application.ViewModels
{
    public class SatConcentradorViewModel
    {
        public Guid SatConcentradorId { get; set; }
        public SatOperacaoEnumView SatOperacao { get; set; }
        public string NumeroVenda { get; set; }
        public string SolicitanteName { get; set; }
        public int Situacao { get; set; }
        public string Mensagem { get; set; }
        public bool Ok { get; set; }
        public string XmlAutorizacao { get; set; }
        public string XmlCancelamento { get; set; }

    }
}