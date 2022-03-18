using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class ClienteDeliveryRepository : RepositoryBase, IClienteDeliveryRepository
    {
        public ClienteDelivery GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ClienteDelivery> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public ClienteDelivery ObterPorFone(string fone)
        {
            var sql = new StringBuilder().AppendLine("Select");
            sql.AppendLine("Codigo as ClienteDeliveryId,");
            sql.AppendLine("Nome,");
            sql.AppendLine("Fone as Telefone,");
            sql.AppendLine("Endereco,");
            sql.AppendLine("Bairro,");
            sql.AppendLine("Cidade,");
            sql.AppendLine("Cep,");
            sql.AppendLine("Numero,");
            sql.AppendLine("UF as Uf,");
            sql.AppendLine("ULTIMA_TAXA_ENTREGA as UltimaTaxa,");
            sql.AppendLine("Obs1 as Observacao");
            sql.AppendLine("From Televenda_2 Where Fone Like @fone");

            using (var conn = Connection)
            {
                conn.Open();
                var cliente = conn.Query<ClienteDelivery>(sql.ToString(), new {fone}).FirstOrDefault();
                conn.Close();

                return cliente;
            }
        }

        public decimal TaxaPorBairro(string bairro)
        {
            var sql = "select Isnull(Max(be_taxa), 0) from bairroentrega Where be_nome like @bairro";
            using (var conn = Connection)
            {
                conn.Open();
                var taxa = conn.Query<decimal>(sql, new { bairro }).FirstOrDefault();
                conn.Close();

                return taxa;
            }
        }
    }
}