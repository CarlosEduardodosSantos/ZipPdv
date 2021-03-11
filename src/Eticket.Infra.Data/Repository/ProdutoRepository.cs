using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class ProdutoRepository : RepositoryBase, IProdutoRepository
    {
        public Produto GetById(int id)
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoItens.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto, " +
                          " Prod.USABALANCA as ParaBalanca " +
                          "From Prod  left Join  pdvGrupoItens On  CodProduto = Prod.Codigo  Where Prod.Codigo = @id";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new {id}).FirstOrDefault();
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> GetAll()
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoItens.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto, " +
                          " Prod.USABALANCA as ParaBalanca " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo ";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { });
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> GetByGrupoId(int grupoId)
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoItens.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto, " +
                          " Prod.USABALANCA as ParaBalanca " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  Where idPdvGrupo = @grupoId";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { grupoId });
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> GetByMeioMeio()
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoItens.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto " +
                          "From pdvMeio Inner Join Prod On  CodProduto = Prod.Codigo";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { });
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> ObterPorEan(string ean)
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoItens.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto, " +
                          " Prod.USABALANCA as ParaBalanca " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  Where Prod.Codigo In (" +
                          "Select COD_PRODUTO From PROD_BARRA Where COD_BARRA = @ean)";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { ean });
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> ObterPorNome(string nome)
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoItens.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto, " +
                          " Prod.USABALANCA as ParaBalanca " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  Where Prod.DES_ Like '%'+ @nome + '%'";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { nome });
                cn.Close();

                return produto;
            }
        }

        public string ObterImageProdutoId(int produtoId)
        {
            var sql = "Select prod_img From Prod Where Codigo = @produtoId";
            using (var conn = Connection)
            {
                conn.Open();
                var image = conn.Query<string>(sql, new {produtoId}).FirstOrDefault();
                conn.Close();

                return image;
            }
        }

        public ProdutoTributacao ObterTributacaoPorProdutoId(int produtoId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select");
            sql.AppendLine("PROD_NCM as NcmCodigo,");
            sql.AppendLine("PROD_CSOSN as IcmsCstSaida,");
            sql.AppendLine("PROD_ORIG as Origem,");
            sql.AppendLine("PROD_ALIQ_ICMS1 as IcmsAliquotaSaida,");
            sql.AppendLine("PROD_CSTPIS_ID as PisCofinsCstSaida,");
            sql.AppendLine("PROD_pPIS as PisAliquota,");
            sql.AppendLine("PROD_pCOFINS as CofinsAliquota,");
            sql.AppendLine("PROD_PIBPT as NcmAliquotaFederalNacional,");
            sql.AppendLine("0 as NcmAliquotaEstadual,");
            sql.AppendLine("PROD_CEST as CestCodigo,");
            sql.AppendLine("CFOP_SAT as Cfop");
            sql.AppendLine("From VW_NF_PRODUTOS Where PROD_COD = @produtoId");

            using (var conn = Connection)
            {
                conn.Open();
                var tributacao = conn.Query<ProdutoTributacao>(sql.ToString(), new { produtoId }).FirstOrDefault();
                conn.Close();

                return tributacao;
            }
        }
    }
}