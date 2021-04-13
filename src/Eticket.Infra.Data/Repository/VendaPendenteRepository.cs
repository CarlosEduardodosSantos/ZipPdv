using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eticket.Infra.Data.Repository
{
    public class VendaPendenteRepository : RepositoryBase, IVendaPendenteRepository
    {
        public void Add(Venda venda)
        {
            using (var cn = Connection)
            {
                var sql = "Insert Into Vendas_Pendentes(Codigo, Descricao, Qtde, Desconto, Unit, Total, Sub, IsSubProd, "
                            +"Multiplo, CodProdPrinc, Estoque, Icms, vlvenda2, Tipo, Cliente, Hora_Abertura, VEND, NRO, "
                            +"ESTACAO, PROD_OBS, SEQLANC, data_abertura) Values( "
                            +"@Codigo, @Descricao, @Qtde, @Desconto, @Unit, @Total, @Sub, @IsSubProd, "
                            +"@Multiplo, @CodProdPrinc, @Estoque, @Icms, @vlvenda2, @Tipo, @Cliente, @Hora_Abertura, @VEND, @NRO, "
                            +"@ESTACAO, @PROD_OBS, @SEQLANC, @data_abertura)";

                venda.VendaId = VendaId();

                foreach (var item in venda.VendaItens)
                {
                    var parms = new DynamicParameters();

                    parms.Add("@Codigo", item.ProdutoId);
                    parms.Add("@Descricao", item.DescricaoProduto);
                    parms.Add("@Qtde", item.Quantidade);

                    parms.Add("@Desconto", item.Desconto);
                    parms.Add("@Unit", item.ValorUnitatio);
                    parms.Add("@Total", item.ValorTotal);
                    parms.Add("@Sub", item.ValorTotal);
                    parms.Add("@IsSubProd", 0);
                    parms.Add("@Multiplo", 0);
                    parms.Add("@CodProdPrinc", 0);

                    parms.Add("@Estoque", 0);
                    parms.Add("@Icms", 0);
                    parms.Add("@vlvenda2", item.ValorUnitatio);
                    parms.Add("@Tipo", "C");
                    parms.Add("@Cliente", venda.ClientePendencia); 
                    parms.Add("@Hora_Abertura", DateTime.Now);
                    parms.Add("@VEND", venda.UsuarioId);
                    parms.Add("@NRO", venda.VendaId);
                    parms.Add("@ESTACAO", Environment.MachineName);
                    parms.Add("@PROD_OBS", item.Observacao);
                    parms.Add("@SEQLANC", item.SeqProduto);
                    parms.Add("@data_abertura", DateTime.Now);


                    cn.Open();
                    cn.Query(sql, parms);
                    cn.Close();
                }

            }
        }

        public void ImprimeFichaGr(string pendenciaId)
        {
            using (var cn = Connection)
            {
                var sql = "Exec PROC_GRI_IMPRIME @pendenciaId, 3 ";

                cn.Open();
                cn.Query(sql, new { pendenciaId });
                cn.Close();


            }
        }

        public IEnumerable<Venda> ObterPorNome(string nome)
        {
            throw new System.NotImplementedException();
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

        public bool PendenciaExistente(string nome)
        {
            using (var cn = Connection)
            {
                var sql = "select * from Vendas_Pendentes Where CLIENTE = @nome";

                cn.Open();
                var exists = cn.Query<int>(sql, new { nome }).Any();
                cn.Close();

                return exists;

            }
        }

        public void Remover(Venda venda)
        {
            using (var cn = Connection)
            {
                var sql = "Delete from Vendas_Pendentes Where NRO = @nome";

                cn.Open();
                cn.Query<int>(sql, new { nome = venda.VendaId }).FirstOrDefault();
                cn.Close();

            }
        }

        public int VendaId()
        {
            
            using (var conn = Connection)
            {
                var sql = "select Max(NRO)  as sequencia from Vendas_Pendentes";
                conn.Open();
                var sequencia = conn.Query<int?>(sql).FirstOrDefault();
                conn.Close();

               
                return sequencia+1 ?? 1;

            }
        }
    }
}
