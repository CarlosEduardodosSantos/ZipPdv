﻿using System;

namespace Eticket.Domain.Entity
{
    public class NFce
    {
        public int NfceId { get; set; }
        public int NumeroNfce { get; set; }
        public int Serie { get; set; }
        public int Modelo { get; set; }
        public DateTime DataHora { get; set; }
        public int VendaId { get; set; }
    }
}