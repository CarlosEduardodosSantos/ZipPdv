using System;

namespace Eticket.Domain.Entity
{
    public class RetornoSat
    {
        public int RetornoSatId { get; set; }
        public bool IsOk { get; set; }
        public int VendaId { get; set; }
        public int EmpresaId { get; set; }
        public int Pdv { get; set; }
        public string Funcao { get; set; }
        public int CfeSatNumeroNf { get; set; }
        public string ChaveEletronicaCFeSATNFce { get; set; }
        public string CfeSatNumeroExtrato { get; set; }
        public string XmlSatEnviado { get; set; }
        public string XmlSatAssinado { get; set; }
        public int Secao { get; set; }
        public string NumeroSerie { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataHora { get; set; }
        public decimal ValorCfe { get; set; }
        public string Eeeee { get; set; }
        public string Cccc { get; set; }
        public string Timestampcfe { get; set; }
    }
}