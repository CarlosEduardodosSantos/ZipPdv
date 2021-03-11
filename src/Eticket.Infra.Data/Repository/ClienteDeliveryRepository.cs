﻿using System.Collections.Generic;
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
    }
}