using System;
using System.Linq;
using System.Text;
using Dapper;
using Zip.EticketSub.Model;

namespace Zip.EticketSub.Repository
{
    public class PedidoRepository : BaseRepository
    {
        public int Adicionar(Pedido pedido)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var sql = new StringBuilder();
                var parns = new DynamicParameters();


                var vendaId = "99" + SeqTabela();

                try
                {
                    sql = new StringBuilder();
                    sql.AppendLine("Insert Into Venda_1 (nro,nrocx,data,hora,tipo,vend,Loja,vl_compra,Cod_cli,");
                    sql.AppendLine("Loja_transf,xFrete,xTele,pdv,Pgto,senha,cpfcnpj, IdopVendas_Pendentes, Estacao)");
                    sql.AppendLine("Values");
                    sql.AppendLine("(@nro,@nrocx,@data,@hora,@tipo,@vend,@Loja,@vl_compra,@Cod_cli,");
                    sql.AppendLine("@Loja_transf,@xFrete,@xTele,@pdv,@Pgto,@senha,@cpfcnpj, @IdopVendas_Pendentes, @Estacao)");

                    parns = new DynamicParameters();
                    parns.Add("@nro", vendaId);
                    parns.Add("@nrocx", Program.CaixaId);
                    parns.Add("@data", pedido.DataHora.Date);
                    parns.Add("@hora", pedido.DataHora.TimeOfDay.ToString().Substring(0, 4));
                    parns.Add("@tipo", "V");
                    parns.Add("@vend", Program.VendedorId);
                    parns.Add("@Loja", 1);
                    parns.Add("@vl_compra", pedido.ValorPedido);
                    parns.Add("@Cod_cli", 0);
                    parns.Add("@Loja_transf", 0);
                    parns.Add("@xFrete", 0);
                    parns.Add("@xTele", 0);
                    parns.Add("@pdv", Program.Pdv);
                    parns.Add("@Pgto", pedido.DataHora.Date);
                    parns.Add("@senha", "");
                    parns.Add("@cpfcnpj", pedido.Cpf);
                    parns.Add("@IdopVendas_Pendentes", 0);
                    parns.Add("@Estacao", "APP");


                    conn.Query(sql.ToString(), parns);

                    int seqLanc = 0;
                    foreach (var pedidoItem in pedido.PedidoItems)
                    {


                        sql = new StringBuilder();
                        sql.AppendLine(
                            "Insert Into Venda_2(NRO,QTDE,COD_PROD,UNIT,TOTAL,PERC,VALOR,LOJA,Des_,SEQLANC, DATAHORA, GPI_IMPRIMIR, PROD_OBS, VlCusto)");
                        sql.AppendLine(
                            "Values (@NRO,@QTDE,@COD_PROD,@UNIT,@TOTAL,@PERC,@VALOR,@LOJA,@Des_,@SEQLANC,@DATAHORA,@GPI_IMPRIMIR,@PROD_OBS,@VlCusto)");


                        seqLanc++;

                        parns = new DynamicParameters();

                        var produto = new ProdutoRepository().ObterPorId(pedidoItem.ProdutoId) ?? new ProdutoRepository().ObterPorId(999999);

                        pedidoItem.Descricao = produto.Nome;
                        parns.Add("@nro", vendaId);
                        parns.Add("@QTDE", pedidoItem.Quantidade);
                        parns.Add("@COD_PROD", pedidoItem.ProdutoId);
                        parns.Add("@Des_", pedidoItem.Descricao);

                        
                        parns.Add("@UNIT", pedidoItem.Unitario);
                        parns.Add("@TOTAL", pedidoItem.Total);
                        parns.Add("@PERC", 0);
                        parns.Add("@VALOR", pedidoItem.Total);
                        parns.Add("@LOJA", Program.Loja);
                        parns.Add("@SEQLANC", seqLanc);
                        parns.Add("@DATAHORA", pedido.DataHora);
                        parns.Add("@GPI_IMPRIMIR", 1);
                        parns.Add("@PROD_OBS", pedidoItem.Observacao);
                        parns.Add("@VlCusto", produto?.ValorCusto > 0 ? produto?.ValorCusto : decimal.Parse("0.01"));


                        conn.Query(sql.ToString(), parns);
                    }

                    sql = new StringBuilder();
                    sql.Append("Exec PROC_GRI_IMPRIME @NroOperacao, @TipoOperacao");

                    conn.Query(sql.ToString(), new { NroOperacao = vendaId, TipoOperacao = 0 });

                    return int.Parse(vendaId);
                }
                catch (Exception e)
                {
                    conn.Query("Delete Venda_1 Where Nro = @vendaId", new { vendaId });
                    conn.Query("Delete Venda_2 Where Nro = @vendaId", new { vendaId });
                    conn.Query("Delete Televenda_1 Where Nro_Venda = @vendaId", new { vendaId });
                    conn.Query("Delete MeioMeio Where NroOperacao = @vendaId", new { vendaId });
                    conn.Query("Delete VENDA_4 Where NROVENDA = @vendaId", new { vendaId });

                    throw new Exception(e.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private int SeqTabela()
        {
            var sql = new StringBuilder().AppendLine("UPDATE SEQ_TABELA SET SEQUENCIA = (SEQUENCIA + 1) WHERE TABELA = 'VENDA' AND COLUNA = 'NRO';");
            sql.AppendLine("select SEQUENCIA from seq_tabela where TABELA = 'VENDA' AND COLUNA = 'NRO' ");

            using (var conn = Connection)
            {
                conn.Open();
                using (var result = conn.QueryMultiple(sql.ToString()))
                {
                    var vendaId = result.ReadFirst<int>();
                    conn.Close();

                    return vendaId;
                }
            }
        }

        public int ImprimeSat(int numeroConcentrador, int status, int funcao, string fabricante, int sessao, string codigoAtivacao,
            string chave, string xml, string pcName, int pedidoId, int empresa, int pdv, string numeroSerie)
        {
            var sql = "pr_insert_concentradorsat @cc_concentr, @cc_status, @cc_funcao, @cc_fabr_sat," +
                      "@cc_nsessao, @cc_codativ, @cc_chave, @cc_xml, @cc_mqreq, @cc_nroop, @cc_tipoop," +
                      "@cc_emp, @cc_pdv, @cc_nroseriesat";

            var parns = new DynamicParameters();
            parns.Add("@cc_concentr", numeroConcentrador);
            parns.Add("@cc_status", status);
            parns.Add("@cc_funcao", funcao);
            parns.Add("@cc_fabr_sat", fabricante);
            parns.Add("@cc_nsessao", sessao);
            parns.Add("@cc_codativ", codigoAtivacao);
            parns.Add("@cc_chave", chave);
            parns.Add("@cc_xml", xml);
            parns.Add("@cc_mqreq", pcName);
            parns.Add("@cc_nroop", pedidoId);
            parns.Add("@cc_tipoop", "V");
            parns.Add("@cc_emp", empresa);
            parns.Add("@cc_pdv", pdv);
            parns.Add("@cc_nroseriesat", numeroSerie);

            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<int>(sql).FirstOrDefault();
                conn.Close();

                return result;
            }
        }

        public SatStatus ObterSatusSat(int codigo)
        {
            var sql = "Select cc_retorno, cc_status, cc_sucesso From concentradorSAT Where cc_cod = @codigo";
            using (var conn = Connection)
            {
                conn.Open();
                var status = conn.Query<SatStatus>(sql, new {codigo}).FirstOrDefault();
                conn.Close();

                return status;
            }
        }
    }
}