﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Domain.Entity
{
    public class ProdutoOpcao
    {
        public Guid ProdutosOpcaoId { get; set; }
        public int ProdutosOpcaoTipoId { get; set; }
        public int Situacao { get; set; }
        public int RestauranteId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Sequencia { get; set; }
        public string ProdutoPdv { get; set; }
        public bool Replicar { get; set; }
    }

    public class ProdutoOpcaoTipo
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
        public List<ProdutoOpcao> ProdutoOpcaos { get; set; }
    }
    public class ProdutosOpcaoTipoRelacao
    {
        public Guid Id { get; set; }
        public Guid ProdutosOpcaoId { get; set; }
        public int ProdutoId { get; set; }
    }
}