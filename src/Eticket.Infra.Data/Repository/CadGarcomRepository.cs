using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CadGarcomRepository: RepositoryBase, ICadGarcomRepository
    {
        public IEnumerable<CadGarcom> GetAll()
        {
            throw new NotImplementedException();
        }

        public CadGarcom GetById(int id)
        {
            var sql = "Select * From CadGarcom Where IdGarcom = @id and Status = 'A'";
            using (var conn = Connection)
            {
                conn.Open();
                var cadgarcom = conn.Query<CadGarcom>(sql, new { id }).FirstOrDefault();
                conn.Close();

                return cadgarcom;
            }
        }

        public IEnumerable<CadGarcom> ObterGarcons()
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select * from CadGarcom where Status = 'A'");
            using (var conn = Connection)
            {
                conn.Open();
                var garcons = conn.Query<CadGarcom>(sql.ToString());
                conn.Close();
                return garcons;
            }
        }
    }
}
