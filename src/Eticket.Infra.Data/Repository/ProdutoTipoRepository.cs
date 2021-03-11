using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface;

namespace Eticket.Infra.Data.Repository
{
    public class ProdutoTipoRepository : RepositoryBase, IProdutoTipoRepository
    {
        public ProdutoTipo GetById(int id)
        {
            var sql = "select Tipo, des_ as Descricao from TAB_TIPO";
            using (var conn = Connection)
            {
                conn.Open();
                var tipo = conn.Query<ProdutoTipo>(sql).FirstOrDefault();
                conn.Close();

                return tipo;
            }
        }

        public IEnumerable<ProdutoTipo> GetAll()
        {
            var sql = "select Tipo, des_ as Descricao from TAB_TIPO";
            using (var conn = Connection)
            {
                conn.Open();
                var tipos = conn.Query<ProdutoTipo>(sql);
                conn.Close();

                return tipos;
            }
        }
    }
}