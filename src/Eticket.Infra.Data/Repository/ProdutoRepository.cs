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
                          " Prod.USABALANCA as ParaBalanca, " +
                          " Prod.QuantidadeFixo as QuantidadeFixo, " +
                          " pdvGrupoItens.HabProd as Visivel " +
                          "From Prod  left Join  pdvGrupoItens On  CodProduto = Prod.Codigo  Where Prod.Codigo = @id";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> GetAll(int loja)
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
                          " Prod.USABALANCA as ParaBalanca, " +
                          " Prod.QuantidadeFixo as QuantidadeFixo, " +
                          " pdvGrupoItens.HabProd as Visivel " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo " +
                          "And hab_loja" + loja + " = 1";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { });
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> GetByGrupoId(int loja, int grupoId)
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
                          " Prod.USABALANCA as ParaBalanca, " +
                          " Prod.QuantidadeFixo as QuantidadeFixo, " +
                          " pdvGrupoItens.HabProd as Visivel " +
                          " From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  " +
                          " Where idPdvGrupo = @grupoId " +
                          "And hab_loja" + loja + " = 1";

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

        public IEnumerable<Produto> ObterPorEan(int loja, string ean)
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
                          " Prod.USABALANCA as ParaBalanca, " +
                          " Prod.QuantidadeFixo as QuantidadeFixo, " +
                          " pdvGrupoItens.HabProd as Visivel " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  " +
                          "Where Prod.Codigo In (" +
                          "Select COD_PRODUTO From PROD_BARRA Where COD_BARRA = @ean) " +
                          "And hab_loja"+loja+" = 1";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { ean });
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> ObterPorNome(int loja, string nome)
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
                          " Prod.USABALANCA as ParaBalanca, " +
                          " Prod.QuantidadeFixo as QuantidadeFixo, " +
                          " pdvGrupoItens.HabProd as Visivel " +
                          "From pdvGrupoItens Inner Join Prod On  CodProduto = Prod.Codigo  " +
                          "Where Prod.DES_ Like '%'+ @nome + '%'";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { nome });
                cn.Close();

                return produto;
            }
        }
        public IEnumerable<Produto> ObterMaisVendidos()
        {
            using (var cn = Connection)
            {
                var sql = SelectBase() +
                          " Where Prod.codigo in (select top 24 COD_PROD from venda_2 Inner Join pdvGrupoItens On venda_2.COD_PROD = pdvGrupoItens.CodProduto group by venda_2.COD_PROD order by sum(qtde) desc)";

                cn.Open();
                var produto = cn.Query<Produto>(sql);
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> ObterAbaixoDoMinimo()
        {
            using (var cn = Connection)
            {
                var sql = SelectBase() +
                          " Where Isnull(QTDE1,0) > 0 And Isnull(QTDE1,0) < Isnull(QTDE_MIN,0)";

                cn.Open();
                var produto = cn.Query<Produto>(sql);
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> ObterEmFalta()
        {
            using (var cn = Connection)
            {
                var sql = SelectBase() +
                          " Where Isnull(QTDE1,0) <= 0";

                cn.Open();
                var produto = cn.Query<Produto>(sql);
                cn.Close();

                return produto;
            }
        }

        public IEnumerable<Produto> ObterEmExesso()
        {
            using (var cn = Connection)
            {
                var sql = SelectBase() +
                          "Where Isnull(QTDE1,0) > Isnull(Qtde_Max_1,0)";

                cn.Open();
                var produto = cn.Query<Produto>(sql);
                cn.Close();

                return produto;
            }
        }

        private static string SelectBase()
        {
            return "Select Prod.Codigo as ProdutoId, " +
                        " Prod.Tipo as ProdutoTipo, " +
                        " Prod.DES_ as Descricao, " +
                        " Prod.Qtde1 as Estoque, " +
                        " Situacao = Case When Isnull(Prod.Ativo,'N') = 'S' Then 1 Else 2 End, " +
                        " Prod.Grupo as GrupoId, " +
                        " 0 as GrupoPdvId, " +
                        " Prod.DEPTO as DeptoId, " +
                        " Prod.SECAO as SecaoId, " +
                        " Prod.Unidade, " +
                        " Prod.VLVENDA as ValorVenda, " +
                        " Isnull(Prod.VLCUSTO,0) as ValorCusto, " +
                        " Isnull(pontos.PONTOS,0) as Pontos, " +
                        " Isnull(Prod.USABALANCA, 0) as UsaBalanca," +
                        " Isnull(Prod.Qtde_Min, 0) as EstoqueMin," +
                        " Isnull(Prod.Qtde_Max, 0) as EstoqueMax," +
                        " Isnull(Prod.ValorFidelidade, 0) as ValorFidelidade," +
                        " Isnull(Prod.VLVENDA2, 0) as ValorPromocao," +
                        " Isnull(Prod.Fornec, 0) as FornecedorId," +
                        " TributacaoId as TributacaoId," +
                        " ProdutoGuid," +
                        " IsPos," +
                        " DTCAD as DataCadastro," +
                        " REGISTRO as Id " +
                        " From Prod  " +
                        " Left Join pontos On prod.COD_PONTOS = pontos.CODIGO ";
        }
        public string ObterImageProdutoId(int produtoId)
        {
            var sql = "Select prod_img From Prod Where Codigo = @produtoId";
            using (var conn = Connection)
            {
                conn.Open();
                var image = conn.Query<string>(sql, new { produtoId }).FirstOrDefault();
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

        public IEnumerable<ProdutoObservacao> ObterProdutoObservacao(int grupoId)
        {
            var sql = "select IdObs as ObservacaoId, Observacao as Descricao, Sequencia as Ranking from pdvObservacao Where pdvGrupo = @grupoId Order By sequencia";
            using (var conn = Connection)
            {
                conn.Open();
                var observacoes = conn.Query<ProdutoObservacao>(sql, new { grupoId });
                conn.Close();

                return observacoes;
            }
        }

        public IEnumerable<Produto> GetSugestaoByGrupoId(int grupoId)
        {
            using (var cn = Connection)
            {
                var sql = "Select Prod.Codigo as ProdutoId, " +
                          " Prod.Tipo as ProdutoTipo, " +
                          " pdvGrupoSugestoes.idPdvGrupo as GrupoId, " +
                          " Prod.Unidade, " +
                          " Prod.DES_ as Descricao, " +
                          " Prod.VLVENDA as ValorVenda, " +
                          " Prod.VLCUSTO as ValorCusto, " +
                          " Prod.USABALANCA as ParaBalanca, " +
                          " Prod.QuantidadeFixo as QuantidadeFixo, " +
                          " pdvGrupoItens.HabProd as Visivel " +
                          "From pdvGrupoSugestoes Inner Join Prod On  CodProduto = Prod.Codigo  Where idPdvGrupo = @grupoId";

                cn.Open();
                var produto = cn.Query<Produto>(sql, new { grupoId });
                cn.Close();

                return produto;
            }
        }
    }
}