using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class VendaFichaRepository : RepositoryBase, IVendaFichaRepository
    {
        public void Add(VendaFicha vendaFicha)
        {
            using (var cn = Connection)
            {
                var sql = "Exec PR_Insert_Ficha " +
                          " @nro = @Ficha, " +
                          " @Loja = @Loja, " +
                          " @Qtde = @Quantidade, " +
                          " @cod_prod = @ProdutoId, " +
                          " @unit = @ValorUnitatio, " +
                          " @valor = @ValorTotal, " +
                          " @des_ = @NomeProduto, " +
                          " @vend = @VendedorId, " +
                          " @perc = 0, " +
                          " @Sabor = 0, " +
                          " @SeqLanc = @Sequencia, " +
                          " @PROD_OBS = @Observacao, " +
                          " @idpromocao = 0, " +
                          " @IdMesa = 0," +
                          " @LocalFicha = '', " +
                          " @Estacao = ''";

                cn.Open();
                cn.Query(sql, new
                {
                    vendaFicha.Ficha,
                    vendaFicha.Loja,
                    vendaFicha.Quantidade,
                    vendaFicha.ProdutoId,
                    vendaFicha.ValorUnitatio,
                    vendaFicha.ValorTotal,
                    vendaFicha.NomeProduto,
                    vendaFicha.VendedorId,
                    vendaFicha.Sequencia,
                    vendaFicha.Observacao,
                    //vendaFicha.ClienteFichaId
                });
                cn.Close();
            }
        }

        public void Remover(VendaFicha vendaFicha)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<VendaFicha> ObterPorFicha(string ficha)
        {
            using (var cn = Connection)
            {
                var sql = "Select Inc_venda2 as FichaItemId, " +
                          " Nro as Ficha, " +
                          " Loja, " +
                          " Cod_prod as ProdutoId, " +
                          " Des_ as NomeProduto, " +
                          " Qtde as Quantidade, " +
                          " Unit as ValorUnitatio, " +
                          " Total as ValorTotal, " +
                          " Vend as VendedorId, " +
                          " SEQLANC as Sequencia, " +
                          " DATAHORA as DataHora," +
                          " ClienteFichaId " +
                          "From Venda_3 Where Nro = @ficha";

                cn.Open();
                var fichaItens = cn.Query<VendaFicha>(sql, new {ficha});
                cn.Close();

                return fichaItens;
            }
        }

        public int ObterUltimaSequencia(string ficha)
        {
            using (var cn = Connection)
            {
                var sql = "Select Max(SEQLANC) From Venda_3 Where Nro = @ficha ";

                cn.Open();
                var ultimaSequencia = cn.Query<int>(sql, new {ficha}).FirstOrDefault();
                cn.Close();

                return ultimaSequencia;

            }
        }

        public void ImprimeFichaGr(string ficha)
        {
            using (var cn = Connection)
            {
                var sql = "Exec PROC_GRI_IMPRIME @ficha, 2 ";

                cn.Open();
                cn.Query(sql, new { ficha });
                cn.Close();


            }
        }
    }
}