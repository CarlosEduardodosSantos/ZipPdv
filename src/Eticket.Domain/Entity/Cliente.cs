﻿

namespace Eticket.Domain.Entity
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Numero { get; set; }
        public decimal Limite { get; set; }
    }
}
