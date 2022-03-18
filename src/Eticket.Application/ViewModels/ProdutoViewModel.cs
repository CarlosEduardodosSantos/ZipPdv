using System;
using System.Collections.Generic;

namespace Eticket.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            ProdutoGuid = Guid.NewGuid();
            DataCadastro = DateTime.Now.Date;
        }
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public SituacaoCadastroEnuView Situacao { get; set; }
        public string ProdutoTipo { get; set; }
        public string Descricao { get; set; }
        public decimal Estoque { get; set; }
        public int UnidadeId { get; set; }
        public string Unidade { get; set; }
        public int GrupoId { get; set; }
        public string Grupo { get; set; }
        public int DeptoId { get; set; }
        public string Depto { get; set; }
        public int SecaoId { get; set; }
        public string Secao { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCusto { get; set; }
        public string Imagem { get; set; }
        public bool UsaBalanca { get; set; }
        public int Pontos { get; set; }
        public decimal EstoqueMin { get; set; }
        public decimal EstoqueMax { get; set; }
        public decimal ValorFidelidade { get; set; }
        public decimal ValorPromocao { get; set; }
        public DateTime DataUltVenda { get; set; }
        public Guid TributacaoId { get; set; }
        public int FornecedorId { get; set; }
        public Guid ProdutoGuid { get; set; }
        public bool IsPos { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool IsMeioMeio => ProdutoTipo == "M";
        public bool ParaBalanca { get; set; }
        public bool BalancaEtiqueta { get; set; }
        public decimal PesoQuantidadeFixo { get; set; }
        public bool QuantidadeFixo { get; set; }
        public decimal ValorUnitBalanca { get; set; }
        public bool Visivel { get; set; }
        public ICollection<ProdutoComplementoViewModel> ProdutoComplementos { get; set; }
    }
}