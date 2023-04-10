using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CadMesasRepository : RepositoryBase, ICadMesasRepository
    {
        public IEnumerable<CadMesas> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public CadMesas GetById(int id)
        {
            var sql = "Select * From CadMesas Where IdMesa = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var cadmesa = conn.Query<CadMesas>(sql, new { id }).FirstOrDefault();
                conn.Close();

                return cadmesa;
            }
        }

        public IEnumerable<CadMesas> ObterMesas()
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select * from CadMesas");
            using (var conn = Connection)
            {
                conn.Open();
                var mesas = conn.Query<CadMesas>(sql.ToString());
                conn.Close();
                return mesas;
            }
        }

        public IEnumerable<CadMesas> ObterMesasDisponiveis()
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select * from CadMesas where status = 1");
            using (var conn = Connection)
            {
                conn.Open();
                var mesas = conn.Query<CadMesas>(sql.ToString());
                conn.Close();
                return mesas;
            }
        }

        public void AlterarStatusMesa(CadMesas cadmesa)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("update CadMesas set Status = @Status");
                sql.AppendLine("where IdMesa = @IdMesa");

                var parms = new DynamicParameters();
                parms.Add("@Status", cadmesa.Status);
                parms.Add("@IdMesa", cadmesa.IdMesa);

                using (var conn = Connection)
                {
                    conn.Open();
                    conn.Query(sql.ToString(), parms);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }

        public void IncluirOpMesa1(CadMesas cadmesa)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("update CadMesas set OpMesa1Atual = @OpMesa1Atual");
                sql.AppendLine("where IdMesa = @IdMesa");

                var parms = new DynamicParameters();
                parms.Add("@OpMesa1Atual", cadmesa.OpMesa1Atual);
                parms.Add("@IdMesa", cadmesa.IdMesa);

                using (var conn = Connection)
                {
                    conn.Open();
                    conn.Query(sql.ToString(), parms);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
