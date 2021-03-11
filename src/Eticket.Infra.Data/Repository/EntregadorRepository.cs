using System.Collections.Generic;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class EntregadorRepository : RepositoryBase, IEntregadorRepository
    {
        public Entregador GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Entregador> GetAll()
        {
            var sql = "Select Codigo as EntregadorId, Nome as Nome From Televenda_3";
            using (var conn = Connection)
            {
                conn.Open();
                var entragadores = conn.Query<Entregador>(sql);
                conn.Close();

                return entragadores;
            }
        }
    }
}