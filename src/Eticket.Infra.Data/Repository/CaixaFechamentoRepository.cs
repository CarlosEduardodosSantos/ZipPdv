using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CaixaFechamentoRepository : RepositoryBase, ICaixaFechamentoRepository
    {
        public CaixaFechamento GetById(int id)
        {
            var sql = "Select * From CaixaFechamentos Where CaixaFechamentoId = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var caixaFechamento = conn.Query<CaixaFechamento>(sql, new {id}).FirstOrDefault();
                conn.Close();

                return caixaFechamento;
            }
        }

        public IEnumerable<CaixaFechamento> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CaixaFechamento> ObterPorCaixaId(int caixaId)
        {
            var sql = "Select * From CaixaFechamentos Where CaixaId = @caixaId";
            using (var conn = Connection)
            {
                conn.Open();
                var caixaFechamentos = conn.Query<CaixaFechamento>(sql, new { caixaId });
                conn.Close();

                return caixaFechamentos;
            }
        }
    }
}