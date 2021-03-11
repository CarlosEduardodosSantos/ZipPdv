using System;

namespace Zip.Sat.Libraries.Model
{
    public class ProdutoSatModel
    {
        public ProdutoSatModel()
        { }

        ProdutoTributacaoSatModel produtoTributacao;

        public long ProdutoID { get; set; }
        public string CodBarra { get; set; }
        public string Descricao { get; set; }
        public string RefFab { get; set; }
        public string Tamanho { get; set; }
        public string Cor { get; set; }
        public string Ref { get; set; }
        public string Marca { get; set; }
        public int IDGrupo { get; set; }
        public int Est_Loja2 { get; set; }
        public int Est_Max { get; set; }
        public decimal MaxComissao { get; set; }
        public decimal MinComissao { get; set; }
        public decimal VdEspecial { get; set; }
        public decimal Ipi { get; set; }
        public int IDCadTributacao { get; set; }
        public long IDSubGrupo { get; set; }
        public decimal Margem { get; set; }
        public decimal Vl_Custo { get; set; }
        public decimal Vl_Venda { get; set; }
        public decimal Desconto { get; set; }
        public DateTime Dt_Alt { get; set; }
        public string Obs { get; set; }
        public decimal VdPrazo { get; set; }
        public long IDSubGrupo1 { get; set; }
        public long IDFornecedor { get; set; }
        public string TabIcms { get; set; }
        public int Etq { get; set; }
        public int NcmCodigo { get; set; }
        public decimal NcmIbpt { get; set; }
        public string Unidade { get; set; }
        public ProdutoTributacaoSatModel ProdutoTributacao
        {
            get
            {
                if (produtoTributacao == null)
                    produtoTributacao = new ProdutoTributacaoSatModel();
                return produtoTributacao;
            }
            set { produtoTributacao = value; }
        }
    }
}