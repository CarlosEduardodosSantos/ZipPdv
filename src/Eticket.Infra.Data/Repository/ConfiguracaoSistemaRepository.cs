using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eticket.Infra.Data.Repository
{
    public class ConfiguracaoSistemaRepository : RepositoryBase, IConfiguracaoSistemaRepository
    {

        public IEnumerable<ConfiguracaoSistema> GetAll()
        {
            var sql = "select * from configuracoes";
            using (var conn = Connection)
            {
                conn.Open();
                var configuracoes = conn.Query<ConfiguracaoSistema>(sql);
                conn.Close();

                return configuracoes;
            }
        }

        public ConfiguracaoSistema GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ConfiguracaoSistema ObterByValiavel(string variavel)
        {
            var sql = "select * from configuracoes where variavel like @variavel";
            using (var conn = Connection)
            {
                conn.Open();
                var configuracao = conn.Query<ConfiguracaoSistema>(sql, new { variavel }).FirstOrDefault();
                conn.Close();

                return configuracao;
            }
        }
    }
}
