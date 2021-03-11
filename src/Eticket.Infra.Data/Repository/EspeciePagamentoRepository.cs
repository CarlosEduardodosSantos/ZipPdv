using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class EspeciePagamentoRepository : RepositoryBase, IEspeciePagamentoRepository
    {
        public EspeciePagamento GetById(int id)
        {
            var sql = "Select * From EspeciePagamentos Where EspeciePagamentoId = id";
            using (var conn = Connection)
            {
                conn.Open();
                var especie = conn.Query<EspeciePagamento>(sql, new {id}).FirstOrDefault();
                conn.Close();
                return especie;
            }
        }

        public IEnumerable<EspeciePagamento> GetAll()
        {
            var sql = "Select * From EspeciePagamentos";
            using (var conn = Connection)
            {
                conn.Open();
                var especies = conn.Query<EspeciePagamento>(sql);
                conn.Close();
                return especies;
            }
        }
    }
}