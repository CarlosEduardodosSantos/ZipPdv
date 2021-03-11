using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class FichaRepository : RepositoryBase, IFichaRepository
    {

        public Ficha GetByFichaId(string fichaId)
        {
            using (var cn = Connection)
            {
                var sql = "Select * From Ficha " +
                          " Left Join ClienteFicha On Ficha.ClienteFichaId = ClienteFicha.ClienteFichaId " +
                          " Where Ficha.FichaNumero = @fichaId";

                cn.Open();
                var ficha = cn.Query<Ficha, ClienteFicha, Ficha>(sql,
                    (f, c) =>
                    {
                        f.ClienteFicha = c;
                        return f;
                    }, 
                    new { fichaId }, splitOn: "FichaId, ClienteFichaId").FirstOrDefault();
                cn.Close();

                return ficha;
            }
        }

        public IEnumerable<Ficha> GetAll()
        {
            using (var cn = Connection)
            {
                var sql = "Select * From Ficha " +
                          " Left Join ClienteFicha On Ficha.ClienteFichaId = ClienteFicha.ClienteFichaId ";

                cn.Open();
                var fichas = cn.Query<Ficha>(sql, new {});
                cn.Close();

                return fichas;
            }
        }
    }
}