﻿namespace Eticket.Domain.Entity
{
    public class ProdutoGrupo
    {
        public int GrupoId { get; set; }
        public string Descricao { get; set; }
        public int Posicao { get; set; }
        public string Imagem { get; set; }
        public string GrupoCor { get; set; }
        public bool IsPadrao { get; set; }
        public bool HabTotem { get; set; }
        public bool HabElisa { get; set; }
        public bool HabPdv { get; set; }
    }
}