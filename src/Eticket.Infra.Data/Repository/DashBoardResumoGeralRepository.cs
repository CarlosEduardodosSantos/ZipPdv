using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Eticket.Domain.Entity.DashBoard;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class DashBoardResumoGeralRepository : RepositoryBase, IDashBoardResumoGeralRepository
    {
        public IEnumerable<DashBoardProduto> ObterProdutosEstoqueBaixo(int empresaId)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("Codigo as ProdutoId,");
            sql.AppendLine("Des_ as ProdutoNome,");
            sql.AppendLine($"Qtde{empresaId} as Quantidade,");
            sql.AppendLine($"Isnull(Qtde_Min,0) as QuantidadeMim,");
            sql.AppendLine($"Isnull(Qtde_Max,0) as QuantidadeMax,");
            sql.AppendLine("VLVENDA as ValorVenda,");
            sql.AppendLine("VLCUSTO as ValorCusto");
            sql.AppendLine("From Prod");
            sql.AppendLine($"Where Isnull(Qtde_Min,0) > 0 And Isnull(Qtde{empresaId},0) <= Isnull(Qtde_Min,0)");

            using (var conn = Connection)
            {
                conn.Open();
                var produtos = conn.Query<DashBoardProduto>(sql.ToString());
                conn.Close();

                return produtos;
            }

        }

        public IEnumerable<DashBoardProduto> ObterProdutosEstoqueExcesso(int empresaId)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("Codigo as ProdutoId,");
            sql.AppendLine("Des_ as ProdutoNome,");
            sql.AppendLine($"Qtde{empresaId} as Quantidade,");
            sql.AppendLine($"Isnull(Qtde_Min,0) as QuantidadeMim,");
            sql.AppendLine($"Isnull(Qtde_Max,0) as QuantidadeMax,");
            sql.AppendLine("VLVENDA as ValorVenda,");
            sql.AppendLine("VLCUSTO as ValorCusto");
            sql.AppendLine("From Prod");
            sql.AppendLine($"Where Isnull(Qtde_Max,0) > 0 And Isnull(Qtde{empresaId},0) >= Isnull(Qtde_Max,0)");

            using (var conn = Connection)
            {
                conn.Open();
                var produtos = conn.Query<DashBoardProduto>(sql.ToString());
                conn.Close();

                return produtos;
            }
        }

        public IEnumerable<DashBoardProduto> ObterProdutosFalta(int empresaId)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("Codigo as ProdutoId,");
            sql.AppendLine("Des_ as ProdutoNome,");
            sql.AppendLine($"Qtde{empresaId} as Quantidade,");
            sql.AppendLine($"Isnull(Qtde_Min_{empresaId},0) as QuantidadeMim,");
            sql.AppendLine($"Isnull(Qtde_Max_{empresaId},0) as QuantidadeMax,");
            sql.AppendLine("VLVENDA as ValorVenda,");
            sql.AppendLine("VLCUSTO as ValorCusto");
            sql.AppendLine("From Prod");
            sql.AppendLine($"Where Isnull(Qtde{empresaId},0) <= 0");

            using (var conn = Connection)
            {
                conn.Open();
                var produtos = conn.Query<DashBoardProduto>(sql.ToString());
                conn.Close();

                return produtos;
            }
        }

        public IEnumerable<DashBoardVendaCompraProduto> ObterProdutosVendidoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine($"Venda_1.Nro as Id,");
            sql.AppendLine($"Venda_2.COD_PROD as ProdutoId,");
            sql.AppendLine($"Venda_2.Des_ as ProdutoNome,");
            sql.AppendLine($"Sum(Isnull(Venda_2.QTDE,0)) as Quantidade,");
            sql.AppendLine($"Sum(Isnull(Venda_2.PERC,0)) as Desconto,");
            sql.AppendLine($"Sum(Isnull(Venda_2.UNIT,0)) as ValorVenda,");
            sql.AppendLine($"Sum(Isnull(Venda_2.VlCusto,0)) as ValorCusto");
            sql.AppendLine($"From Venda_1");
            sql.AppendLine($"Inner Join Venda_2 On Venda_1.Nro = Venda_2.Nro");
            sql.AppendLine($"Where Venda_1.Loja = @empresaId And Venda_1.Data Between @dataInicio And @dataFinal");
            sql.AppendLine($"Group By Venda_1.Nro, Venda_2.COD_PROD, Venda_2.Des_");
            sql.AppendLine($"Order By Quantidade desc");

            using (var conn = Connection)
            {
                conn.Open();
                var produtos = conn.Query<DashBoardVendaCompraProduto>(sql.ToString(), new {empresaId, dataInicio, dataFinal});
                conn.Close();

                return produtos;
            }
        }

        public IEnumerable<DashBoardVendaCompraProduto> ObterProdutosCompradoPorData(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine($"CP1.CP1_NRO as Id,");
            sql.AppendLine($"CP2.CP2_PROD as ProdutoId,");
            sql.AppendLine($"Prod.DES_ as ProdutoNome,");
            sql.AppendLine($"Sum(Isnull(CP2.CP2_QTDE,0)) as Quantidade,");
            sql.AppendLine($"Sum(Isnull(CP2.CP2_vDESC,0)) as Desconto,");
            sql.AppendLine($"Sum(Isnull(CP2.CP2_UNITARIO,0)) as ValorVenda,");
            sql.AppendLine($"Sum(Isnull(CP2.CP2_CUSTO,0)) as ValorCusto");
            sql.AppendLine($"From CP1");
            sql.AppendLine($"Inner Join CP2 On CP1.CP1_NRO = CP2.CP1_NRO");
            sql.AppendLine($"Inner Join Prod On CP2.CP2_PROD = Prod.CODIGO");
            sql.AppendLine($"Where CP1.CP1_LOJA = @empresaId And CP1.CP1_DTLANC Between @dataInicio And @dataFinal");
            sql.AppendLine($"Group By CP1.CP1_NRO, CP2.CP2_PROD, Prod.DES_");
            sql.AppendLine($"Order By Quantidade desc");

            using (var conn = Connection)
            {
                conn.Open();
                var produtos = conn.Query<DashBoardVendaCompraProduto>(sql.ToString(), new { empresaId, dataInicio, dataFinal });
                conn.Close();

                return produtos;
            }
        }

        public IEnumerable<DashBoardVenda> ObterVendasPorData(int empresaId, DateTime dataInicio, DateTime dataFinal)
        {
            var sql = @"Select 
	                        Venda_1.Nro as VendaId,
	                        Venda_1.Data + Cast(Venda_1.hora as time)  DataHora,
	                        Isnull(CLIENTE.CODIGO, 0) as ClienteId,
	                        Isnull(Cliente.Nome,'') as Cliente,
	                        Sum(Venda_2.TOTAL) as ValorTotal ,
	                        Sum(Isnull(Venda_2.VlCusto,0)*Venda_2.QTDE) as CustoTotal
                        From Venda_1
                        Inner Join Venda_2 On Venda_1.NRO = Venda_2.NRO
                        Left Join Cliente On Venda_1.COD_CLI = CLIENTE.CODIGO
                        Where Venda_1.Loja = @empresaId And Venda_1.DATA Between @dataInicio And @dataFinal
                        Group By Venda_1.Nro, Venda_1.Data, Venda_1.hora, Cliente.Nome, CLIENTE.CODIGO";

            using (var conn = Connection)
            {
                conn.Open();
                var vendas = conn.Query<DashBoardVenda>(sql, new { empresaId, dataInicio, dataFinal });
                conn.Close();

                return vendas;
            }
        }
    }
}