using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zip.Utils;

namespace Eticket.Infra.Data.Repository
{
    public class VendaPendenteRepository : RepositoryBase, IVendaPendenteRepository
    {
        public void Add(VendaPendente venda)
        {
            using (var cn = Connection)
            {
                var sql = "Insert Into Vendas_Pendentes(Codigo, Descricao, Qtde, Desconto, Unit, Total, Sub, IsSubProd, "
                            +"Multiplo, CodProdPrinc, Estoque, Icms, vlvenda2, Tipo, Cliente, Hora_Abertura, VEND, NRO, "
                            +"ESTACAO, PROD_OBS, SEQLANC, data_abertura, senha) Values( "
                            +"@Codigo, @Descricao, @Qtde, @Desconto, @Unit, @Total, @Sub, @IsSubProd, "
                            +"@Multiplo, @CodProdPrinc, @Estoque, @Icms, @vlvenda2, @Tipo, @Cliente, @Hora_Abertura, @VEND, @NRO, "
                            +"@ESTACAO, @PROD_OBS, @SEQLANC, @data_abertura, @senha)";

                venda.Nro = venda.Nro > 0 ? venda.Nro : VendaId();
                venda.Senha = ObterSenha();

                var parms = new DynamicParameters();

                parms.Add("@Codigo", venda.ProdutoId);
                parms.Add("@Descricao", venda.Produto);
                parms.Add("@Qtde", venda.Quantidade);

                parms.Add("@Desconto", venda.Desconto);
                parms.Add("@Unit", venda.Unitario);
                parms.Add("@Total", venda.Total);
                parms.Add("@Sub", venda.SubTotal);
                parms.Add("@IsSubProd", 0);
                parms.Add("@Multiplo", 0);
                parms.Add("@CodProdPrinc", 0);

                parms.Add("@Estoque", 0);
                parms.Add("@Icms", 0);
                parms.Add("@vlvenda2", venda.ValorVenda);
                parms.Add("@Tipo", "C");
                parms.Add("@Cliente", venda.Cliente);
                parms.Add("@Hora_Abertura", venda.Hora);
                parms.Add("@VEND", venda.UsuarioId);
                parms.Add("@NRO", venda.Nro);
                parms.Add("@ESTACAO", Environment.MachineName);
                parms.Add("@PROD_OBS", venda.Observacao);
                parms.Add("@SEQLANC", venda.SeqProduto);
                parms.Add("@data_abertura", venda.DataHora.Date);
                parms.Add("@senha", venda.Senha);

                cn.Open();
                cn.Query(sql, parms);
                cn.Close();
            }
        }
        public IEnumerable<VendaPendente> ObterPorNome(string nome)
        {
            using (var cn = Connection)
            {
                var sql = new StringBuilder();
                sql.AppendLine("Select  Chave,");
                sql.AppendLine("NRO,");
                sql.AppendLine("Hora_Abertura as Hora,");
                sql.AppendLine("data_abertura as DataHora,");
                sql.AppendLine("Codigo as ProdutoId,");
                sql.AppendLine("Descricao as Produto,");
                sql.AppendLine("Qtde as Quantidade,");
                sql.AppendLine("Desconto,");
                sql.AppendLine("Unit as Unitario,");
                sql.AppendLine("Total,");
                sql.AppendLine("Sub as SubTotal,");
                sql.AppendLine("vlvenda2 as ValorVenda,");
                sql.AppendLine("Cliente,");
                sql.AppendLine("VEND as usuarioId,");
                
                sql.AppendLine("ESTACAO,");
                sql.AppendLine("PROD_OBS as Observacao,");
                sql.AppendLine("Senha");
                sql.AppendLine("From Vendas_Pendentes Where CLIENTE = @nome");


                cn.Open();
                var vendas = cn.Query<VendaPendente>(sql.ToString(), new { nome });
                cn.Close();

                return vendas;

            }
        }

        public IEnumerable<VendaPendente> ObterTodos()
        {
            using (var cn = Connection)
            {
                var sql = new StringBuilder();
                sql.AppendLine("Select  Chave,");
                sql.AppendLine("NRO,");
                sql.AppendLine("Hora_Abertura as Hora,");
                sql.AppendLine("data_abertura as DataHora,");
                sql.AppendLine("Codigo as ProdutoId,");
                sql.AppendLine("Descricao as Produto,");
                sql.AppendLine("Qtde as Quantidade,");
                sql.AppendLine("Desconto,");
                sql.AppendLine("Unit as Unitario,");
                sql.AppendLine("Total,");
                sql.AppendLine("Sub as SubTotal,");
                sql.AppendLine("vlvenda2 as ValorVenda,");
                sql.AppendLine("Cliente,");
                sql.AppendLine("VEND as usuarioId,");

                sql.AppendLine("ESTACAO,");
                sql.AppendLine("PROD_OBS as Observacao,");
                sql.AppendLine("Senha");
                sql.AppendLine("From Vendas_Pendentes");


                cn.Open();
                var vendas = cn.Query<VendaPendente>(sql.ToString());
                cn.Close();

                return vendas;

            }
        }

        public int ObterUltimaSequencia(string nome)
        {
            using (var cn = Connection)
            {
                var sql = "select Max(SEQLANC) from Vendas_Pendentes Where CLIENTE = @nome";

                cn.Open();
                var ultimaSequencia = cn.Query<int>(sql, new { nome }).FirstOrDefault();
                cn.Close();

                return ultimaSequencia;

            }
        }

        public int PendenciaExistente(string nome)
        {
            using (var cn = Connection)
            {
                var sql = "select Nro from Vendas_Pendentes Where CLIENTE = @nome";

                cn.Open();
                var exists = cn.Query<int>(sql, new { nome }).FirstOrDefault();
                cn.Close();

                return exists;

            }
        }

        public void Remover(int Nro)
        {
            using (var cn = Connection)
            {
                var sql = "Delete from Vendas_Pendentes Where NRO = @Nro";

                cn.Open();
                cn.Query<int>(sql, new { Nro }).FirstOrDefault();
                cn.Close();

            }
        }

        public int VendaId()
        {
            AtualizaVendaId();


            var sql = "Select Isnull(SEQUENCIA,1) from SEQ_TABELA Where TABELA = 'VENDA_PENDENTE' And COLUNA = 'NRO'";
            using (var conn = Connection)
            {
                TryRetry.Do(() => conn.Open(), TimeSpan.FromSeconds(5));

                var sequencia = conn.Query<int>(sql).FirstOrDefault();
                conn.Close();


                return sequencia + 1;

            }
        }

        private void AtualizaVendaId()
        {
            var sql = "Update SEQ_TABELA Set Sequencia = Isnull(Sequencia,0)+1 Where TABELA = 'VENDA_PENDENTE' And COLUNA = 'NRO'";

            using (var conn = Connection)
            {
                TryRetry.Do(() => conn.Open(), TimeSpan.FromSeconds(5));
                conn.Query(sql);
                conn.Close();
            }

        }

        private string ObterSenha()
        {
            var sql = "select Cast(Isnull(valor,0) as Int) from configuracoes where variavel like 'senha'";
            try
            {
                using (var conn = Connection)
                {

                    conn.Open();
                    var senha = conn.Query<int>(sql).FirstOrDefault();

                    senha += 1;

                    //Incrementa valor
                    conn.Execute("Update configuracoes Set valor = @novaSenha where variavel like 'senha'", new { novaSenha = senha });

                    conn.Close();


                    return senha.ToString();

                }

            }
            catch
            {
                return "0";
            }
        }

        public bool GeraImpressaoFechamento(int pendenciaId, int tipoOperacao)
        {
            
            var estacao = Environment.MachineName;
            var sql = "Exec PROC_GRI_IMPRIME_FECHAMENTO @pendenciaId, @tipoOperacao, @estacao, 0";

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    conn.Query(sql, new { pendenciaId, tipoOperacao, estacao });
                    conn.Close();

                    return true;
                }
                catch
                {
                    return false;
                }

            }

        }

        public void NotificarPronto(int nro)
        {
            

            var sql = "Update Vendas_Pendentes  Set Retirar = 1 Where Nro = @nro";

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    conn.Query(sql, new { nro });
                    conn.Close();

            
                }
                catch
                {
                    throw new NotImplementedException();
                }

            }

        }

        public bool GeraImpressaoItem(int pendenciaId, int tipoOperacao)
        {
            using (var cn = Connection)
            {
                var sql = "Exec PROC_GRI_IMPRIME @pendenciaId, @tipoOperacao ";

                cn.Open();
                cn.Query(sql, new { pendenciaId, tipoOperacao });
                cn.Close();

                return true;
            }
        }
    }
}
