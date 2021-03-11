﻿using System;

namespace Eticket.Domain.Entity
{
    public class Ficha
    {
        public int FichaId { get; set; }
        public string FichaNumero { get; set; }
        public int ClienteFichaId { get; set; }
        public DateTime DataChekIn { get; set; }
        public DateTime DataChekOut { get; set; }
        public int Situacao { get; set; }
        public string Observacao { get; set; }

        public ClienteFicha ClienteFicha { get; set; }
    }
}