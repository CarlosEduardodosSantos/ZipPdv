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
    public class OpMesa1Repository : RepositoryBase, IOpMesa1Repository
    {
        public int Abrir(OpMesa1 opmesa)
        {
            try
            {
                int mesaId;
                var sql = new StringBuilder();
                sql.AppendLine("Insert Into OpMesa1(IdGarcom, IdMesa,DthrInicial, QtdePessoas, Dinheiro, Cartao_Debito, Cartao_Credito, Cartao_Consumo)");
                sql.AppendLine("Values (@IdGarcom, @IdMesa, @DthrInicial, @QtdePessoas, @Dinheiro, @Cartao_Debito, @Cartao_Credito, @Cartao_Consumo) SELECT SCOPE_IDENTITY();");

                var parms = new DynamicParameters();
                parms.Add("@IdGarcom", opmesa.IdGarcom);
                parms.Add("@IdMesa", opmesa.IdMesa);
                parms.Add("@DthrInicial", DateTime.Now);
                parms.Add("@QtdePessoas", opmesa.QtdePessoas);
                parms.Add("@Dinheiro", 0);
                parms.Add("@Cartao_Debito", 0);
                parms.Add("@Cartao_Credito", 0);
                parms.Add("@Cartao_Consumo", 0);

                using (var conn = Connection)
                {
                    conn.Open();
                    var value = conn.Query<int>(sql.ToString(), parms).FirstOrDefault();
                    conn.Close();
                    return Convert.ToInt32(value);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Atualizar(OpMesa1 opmesa)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("update OpMesa1 set QtdePessoas = @QtdePessoas, Status = @Status, IdGarcom = @IdGarcom");
                sql.AppendLine("where IdopMesa1 = @IdopMesa1");

                var parms = new DynamicParameters();
                parms.Add("@QtdePessoas", opmesa.QtdePessoas);
                parms.Add("@IdopMesa1", opmesa.IdopMesa1);
                parms.Add("@Status", opmesa.Status);
                parms.Add("@IdGarcom", opmesa.IdGarcom);

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

        public void Transferir(OpMesa1 opmesa)
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("update OpMesa1 set IdMesa = @IdMesa");
                sql.AppendLine("where IdopMesa1 = @IdopMesa1");

                var parms = new DynamicParameters();
                parms.Add("@IdMesa", opmesa.IdMesa);
                parms.Add("@IdopMesa1", opmesa.IdopMesa1);

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

        public void Pagamento(OpMesa1 opmesa)
        {
            if(opmesa.Dinheiro == null)
            {
                opmesa.Dinheiro = 0;
            }
            if (opmesa.Cartao_Debito == null)
            {
                opmesa.Cartao_Debito = 0;
            }
            if (opmesa.Cartao_Credito == null)
            {
                opmesa.Cartao_Credito = 0;
            }
            if (opmesa.Cartao_Consumo == null)
            {
                opmesa.Cartao_Consumo = 0;
            }
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("update OpMesa1 set Dinheiro = @Dinheiro, Cartao_Debito = @Cartao_Debito, Cartao_Credito = @Cartao_Credito, Cartao_Consumo = @Cartao_Consumo");
                sql.AppendLine("where IdopMesa1 = @IdopMesa1");

                var parms = new DynamicParameters();
                parms.Add("@IdopMesa1", opmesa.IdopMesa1);
                parms.Add("@Dinheiro", opmesa.Dinheiro);
                parms.Add("@Cartao_Debito", opmesa.Cartao_Debito);
                parms.Add("@Cartao_Credito", opmesa.Cartao_Credito);
                parms.Add("@Cartao_Consumo", opmesa.Cartao_Consumo);

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

        public IEnumerable<OpMesa1> GetAll()
        {
            throw new NotImplementedException();
        }

        public OpMesa1 GetById(int id)
        {
            var sql = "Select * From OpMesa1 Where IdOpMesa1 = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var cadmesa = conn.Query<OpMesa1>(sql, new { id }).FirstOrDefault();
                conn.Close();

                return cadmesa;
            }
        }
    }
}
