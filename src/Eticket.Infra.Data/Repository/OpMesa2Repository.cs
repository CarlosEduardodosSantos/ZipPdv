using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticket.Infra.Data.Repository
{
    public class OpMesa2Repository : RepositoryBase, IOpMesa2Repository
    {
        public void Abrir(OpMesa2 opmesa)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("Insert Into OpMesa2(IdOpMesa1,Qtde, Valor, Desproduto, VlUnit, Desconto, Obs, Pago)");
                sql.AppendLine("Values (@idOpMesa1, @Qtde, @Valor, @DesProduto, @VlUnit, @Desconto, @Obs, @Pago)");

                var parms = new DynamicParameters();
                parms.Add("@idOpMesa1", opmesa.idOpMesa1);
                parms.Add("@Qtde", opmesa.Qtde);
                parms.Add("@Valor", opmesa.Valor);
                parms.Add("@DesProduto", opmesa.DesProduto);
                parms.Add("@VlUnit", opmesa.VlUnit);
                parms.Add("@Desconto", opmesa.Desconto);
                parms.Add("@Obs", opmesa.Obs);
                parms.Add("@Pago", false);

                using (var conn = Connection)
                {
                    conn.Open();
                    conn.Query(sql.ToString(), parms);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw  ex;
            }
        }

        public IEnumerable<OpMesa2> GetAll()
        {
            throw new NotImplementedException();
        }

        public OpMesa2 GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OpMesa2> PegarItens(int id)
        {
            var sql = "Select * From OpMesa2 Where IdOpMesa1 = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var cadmesa = conn.Query<OpMesa2>(sql.ToString(), new { id });
                conn.Close();


                return cadmesa;
            }
        }

        public void DeletarItem(int id)
        {
            var sql = "Delete From OpMesa2 Where IdOpMesa2 = @id";
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, new { id });
                conn.Close();
            }
        }

        public void Bonificar(int id)
        {
            var sql = "update OpMesa2 set VlUnit = 0.01, Desconto = 0.00 Where IdOpMesa2 = @id";
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, new { id });
                conn.Close();
            }
        }

        public void BonificarMesa(int id)
        {
            var sql = "update OpMesa2 set VlUnit = 0.01, Desconto = 0.00 Where IdOpMesa1 = @id";
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, new { id });
                conn.Close();
            }
        }

        public void PagaItem(int id, string metodo)
        {
            var sql = "update OpMesa2 set pago = 1, metodo = @metodo where IdOpMesa2 = @id";

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, new { id, metodo });
                conn.Close();
            }
        }

        public void EstornaItem(OpMesa2 opmesa)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("Update OpMesa2 set Pago = 0 where IdOpMesa2 = @IdOpMesa2");
                sql.AppendLine("Update OpMesa1 set @Metodo = (@Metodo -= @Valor) where IdOpMesa1 = @IdOpMesa1");

                var parms = new DynamicParameters();
                parms.Add("@idOpMesa1", opmesa.idOpMesa1);
                parms.Add("@idOpMesa2", opmesa.idOpMesa2);
                parms.Add("@Valor", opmesa.Valor);
                parms.Add("@Metodo", opmesa.Metodo);


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

