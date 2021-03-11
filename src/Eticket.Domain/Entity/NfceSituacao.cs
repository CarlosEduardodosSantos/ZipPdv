using System;

namespace Eticket.Domain.Entity
{
    public class NfceSituacao
    {
        public int CodigoSituacao { get; set; }
        public string SituacaoNfe { get; set; }
        public DateTime DataSituacao { get; set; }
    }
}