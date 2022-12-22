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
    public class ClienteRepository : RepositoryBase, IClienteRepository
    {
        public IEnumerable<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        public Cliente GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> ObterPorCodigo(int codigo)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("CODIGO as CODIGO,");
            sql.AppendLine("Nome,");
            sql.AppendLine("Fone1 as Telefone,");
            sql.AppendLine("Endereco,");
            sql.AppendLine("Bairro,");
            sql.AppendLine("Cidade,");
            sql.AppendLine("Cep,");
            sql.AppendLine("End_num as Numero,");
            sql.AppendLine("UF as Uf,");
            sql.AppendLine("CIC as Cpf,");
            sql.AppendLine("LIMITE as LIMITE");
            sql.AppendLine("From cliente Where codigo = @codigo");

            using (var conn = Connection)
            {
                conn.Open();
                var cliente = conn.Query<Cliente>(sql.ToString(), new { codigo });
                conn.Close();

                return cliente;
            }
        }

        public IEnumerable<Cliente> ObterPorNome(string nome)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("CODIGO as CODIGO,");
            sql.AppendLine("Nome,");
            sql.AppendLine("Fone1 as Telefone,");
            sql.AppendLine("Endereco,");
            sql.AppendLine("Bairro,");
            sql.AppendLine("Cidade,");
            sql.AppendLine("Cep,");
            sql.AppendLine("End_num as Numero,");
            sql.AppendLine("UF as Uf,");
            sql.AppendLine("CIC as Cpf,");
            sql.AppendLine("LIMITE as LIMITE");
            sql.AppendLine("From cliente Where Nome Like '%'+ @nome + '%'");

            using (var conn = Connection)
            {
                conn.Open();
                var cliente = conn.Query<Cliente>(sql.ToString(), new { nome });
                conn.Close();

                return cliente;
            }
        }
    }
}
