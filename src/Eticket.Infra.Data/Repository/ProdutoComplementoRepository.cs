using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class ProdutoComplementoRepository : RepositoryBase, IProdutoComplementoRepository
    {
        public ProdutoComplemento GetById(int id)
        {
            using (var cn = Connection)
            {
                var sql = "Select inc_compto as ComplementoId," +
                          "des_ as Descricao," +
                          "GRUPO_PROD as GrupoId," +
                          "Valor " +
                          "From Complemento Where inc_compto = @id";

                cn.Open();
                var complemento = cn.Query<ProdutoComplemento>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return complemento;
            }
        }

        public IEnumerable<ProdutoComplemento> ObterPorGrupoId(int grupoId)
        {
            using (var cn = Connection)
            {
                var sql = "Select inc_compto as ComplementoId," +
                          "des_ as Descricao," +
                          "GRUPO_PROD as GrupoId," +
                          "Valor " +
                          "From Complemento Where GRUPO_PROD = @grupoId";

                cn.Open();
                var complementos = cn.Query<ProdutoComplemento>(sql, new { grupoId });
                cn.Close();

                return complementos;
            }
        }
    }
}