using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class VendaComplementoRepository : RepositoryBase, IVendaComplementoRepository
    {
        public void Add(VendaComplemento vendaComplemento)
        {
            using (var cn = Connection)
            {
                var sql = "INSERT INTO VENDA_4(COMPCOD,  PRODCOD,  SEQLANC,  VALOR, IDOPMESA1, NROCARTAO, NROPENDENTE, NROVENDA) " +
                          " VALUES (@ComplementoId, @ProdutoId, @Sequencia, @Valor, @MesaOperacaoId, @Ficha, @PendenteId, @VendaId ) ";

                cn.Open();
                cn.Query(sql, new
                {
                    vendaComplemento.ComplementoId,
                    vendaComplemento.ProdutoId,
                    vendaComplemento.Sequencia,
                    vendaComplemento.Valor,
                    vendaComplemento.MesaOperacaoId,
                    vendaComplemento.Ficha,
                    vendaComplemento.PendenteId,
                    vendaComplemento.VendaId

                });
                cn.Close();
            }
        }

        public void Remove(VendaComplemento vendaComplemento)
        {
            using (var cn = Connection)
            {
                var sql = "Delete Venda_4 Where Id = @VendaComplementoId ";

                cn.Open();
                cn.Query(sql, new
                {
                    vendaComplemento.VendaComplementoId

                });
                cn.Close();
            }
        }

        public VendaComplemento GetById(int id)
        {
            using (var cn = Connection)
            {
                var sql = GetSelectBase() + "Where Id = @id";

                cn.Open();
                var vendaComplemento = cn.Query<VendaComplemento>(sql, new{ id }).FirstOrDefault();
                cn.Close();

                return vendaComplemento;
            }
        }

        public IEnumerable<VendaComplemento> ObterPorFicha(string ficha)
        {
            using (var cn = Connection)
            {
                var sql = GetSelectBase() + "Where NROCARTAO = @ficha";

                cn.Open();
                var vendaComplementos = cn.Query<VendaComplemento>(sql, new { ficha });
                cn.Close();

                return vendaComplementos;
            }
        }

        public IEnumerable<VendaComplemento> ObterPorPendenciaId(int pendenciaId)
        {
            using (var cn = Connection)
            {
                var sql = GetSelectBase() + "Where NroPendente = @pendenciaId";

                cn.Open();
                var vendaComplementos = cn.Query<VendaComplemento>(sql, new { pendenciaId });
                cn.Close();

                return vendaComplementos;
            }
        }

        public IEnumerable<VendaComplemento> ObterPorMesaId(int mesaId)
        {
            using (var cn = Connection)
            {
                var sql = GetSelectBase() + "Where IDOPMESA1 = @mesaId";

                cn.Open();
                var vendaComplementos = cn.Query<VendaComplemento>(sql, new { mesaId });
                cn.Close();

                return vendaComplementos;
            }
        }

        public IEnumerable<VendaComplemento> ObterPorVendaId(int vendaId)
        {
            using (var cn = Connection)
            {
                var sql = GetSelectBase() + "Where NroVenda = @vendaId";

                cn.Open();
                var vendaComplementos = cn.Query<VendaComplemento>(sql, new { vendaId });
                cn.Close();

                return vendaComplementos;
            }
        }

        private string GetSelectBase()
        {
            return @"Select 
	                    Id as VendaComplementoId,
	                    CompCod as ComplementoId,
	                    ProdCod as ProdutoId,
	                    Valor as Valor,
	                    NROCARTAO as Ficha,
	                    IDOPMESA1 as MesaOperacaoId,
	                    NroPendente as PendenteId,
	                    NroVenda as VendaId, 
	                    SeqLanc as Sequencia
                    From venda_4 ";
        }
    }
}