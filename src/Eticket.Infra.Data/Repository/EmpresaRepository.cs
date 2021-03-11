using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class EmpresaRepository : RepositoryBase, IEmpresaRepository
    {
        public Empresa GetById(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select");
            sql.AppendLine("SIEMP_LOJA as EmpresaId,");
            sql.AppendLine("SIEMP_RAZAOSOCIAL As RazaoSocial,");
            sql.AppendLine("SIEMP_NOMEFANTASIA as Fantasia,");
            sql.AppendLine("SIEMP_CNPJ as Cnpj,");
            sql.AppendLine("SIEMP_IE as Ie,");
            sql.AppendLine("SIEMP_signAC_SAT as SignAC,");
            sql.AppendLine("SIEMP_CRT as Crt,");
            sql.AppendLine("SIEMP_CNPJ_SOFTWAREHOUSE as SoftwareHouseCnpj,");
            sql.AppendLine("SIEMP_COD_ATIV_SAT as SoftwareHouseChaveAtivacao");
            sql.AppendLine("From SIEMP");
            sql.AppendLine("Where SIEMP_LOJA = @id");
            using (var conn = Connection)
            {
                conn.Open();
                var empresa = conn.Query<Empresa>(sql.ToString(), new {id}).FirstOrDefault();
                conn.Close();
                return empresa;
            }
        }

        public IEnumerable<Empresa> GetAll()
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select");
            sql.AppendLine("SIEMP_LOJA as EmpresaId,");
            sql.AppendLine("SIEMP_RAZAOSOCIAL As RazaoSocial,");
            sql.AppendLine("SIEMP_NOMEFANTASIA as Fantasia,");
            sql.AppendLine("SIEMP_CNPJ as Cnpj,");
            sql.AppendLine("SIEMP_IE as Ie,");
            sql.AppendLine("SIEMP_signAC_SAT as SignAC,");
            sql.AppendLine("SIEMP_CRT as Crt,");
            sql.AppendLine("SIEMP_CNPJ_SOFTWAREHOUSE as SoftwareHouseCnpj,");
            sql.AppendLine("SIEMP_COD_ATIV_SAT as SoftwareHouseChaveAtivacao");
            sql.AppendLine("Where SIEMP_LOJA = @id");
            using (var conn = Connection)
            {
                conn.Open();
                var empresa = conn.Query<Empresa>(sql.ToString());
                conn.Close();
                return empresa;
            }
        }
    }
}