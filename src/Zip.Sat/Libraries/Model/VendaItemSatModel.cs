using System;
using System.Collections.ObjectModel;

namespace Zip.Sat.Libraries.Model
{
    public class VendaItemCollection : Collection<VendaItemSatModel>
    { }

    public class VendaItemSatModel
    {
        public VendaItemSatModel()
        { }

        VendaSatModel venda;
        public VendaSatModel Venda
        {
            get
            {
                if (venda == null)
                    venda = new VendaSatModel();
                return venda;
            }
            set { venda = value; }
        }

        ProdutoSatModel produto;
        public ProdutoSatModel Produto
        {
            get
            {
                if (produto == null)
                    produto = new ProdutoSatModel();
                return produto;
            }
            set { produto = value; }
        }

        public long VendaItemID { get; set; }
        public int VendaID { get; set; }
        public int ProdutoID { get; set; }
        public decimal Valor { get; set; }
        public decimal Qtde { get; set; }
        public decimal ValorItem { get; set; }
        public decimal DescontoItem { get; set; }
        public decimal NcmImpostoFederal { get; set; }
        public DateTime DtEntregaCondicionalItem { get; set; }
        public string Ncm { get; set; }
        public decimal NcmAliquotaFederal { get; set; }
        public decimal NcmAliquotaEstadual { get; set; }
        public decimal NcmImpostoEstadual { get; set; }
        public string ProdutoOrigem { get; set; }
        public string IcmsCstCsosn { get; set; }
        public decimal IcmsAliquota { get; set; }
        public decimal IcmsValor { get; set; }
        public string PisCofinsCst { get; set; }
        public decimal PisAliquota { get; set; }
        public decimal PisValor { get; set; }
        public decimal CofinsAliquota { get; set; }
        public decimal CofinsValor { get; set; }
        public string Cfop { get; set; }
        public string CestCodigo { get; set; }
        public string ProdutoDescricao { get { return Produto.Descricao; } }
        public string ProdutoEan { get { return Produto.CodBarra; } }
        public string ProdutoUnidade { get { return Produto.Unidade; } }
    }
}