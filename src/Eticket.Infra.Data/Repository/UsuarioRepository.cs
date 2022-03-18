using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using Zip.Utils;

namespace Eticket.Infra.Data.Repository
{
    public class UsuarioRepository : RepositoryBase, IUsuarioRepository
    {
        public Usuario GetById(int id)
        {
            using (var cn = Connection)
            {
                var sql = "Select  " +
                          "CODIGO as UsuarioId, " +
                          "NOME as Nome, " +
                          "Senha " +
                          "from Recep  Where CODIGO = @id";

                TryRetry.Do(() => cn.Open(), TimeSpan.FromSeconds(5));
                var usuario = cn.Query<Usuario>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return usuario;
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            using (var cn = Connection)
            {
                var sql = "Select  " +
                          "CODIGO as UsuarioId, " +
                          "NOME as Nome, " +
                          "Senha " +
                          "from Recep Where ATIVO = 'SIM'";

                TryRetry.Do(() => cn.Open(), TimeSpan.FromSeconds(5));
                var usuarios = cn.Query<Usuario>(sql, new { });
                cn.Close();

                return usuarios;
            }
        }

        public Usuario GetAutenticacao(string senha)
        {
            using (var cn = Connection)
            {
                var sql = "Select  " +
                          "CODIGO as UsuarioId, " +
                          "NOME as Nome, " +
                          "Senha " +
                          "from Recep  Where Senha = @senha";

                TryRetry.Do(() => cn.Open(), TimeSpan.FromSeconds(5));
                var usuario = cn.Query<Usuario>(sql, new { senha }).FirstOrDefault();
                cn.Close();

                return usuario;
            }
        }

        public bool VerificaPrivilegio(string privilegio, int id)
        {
            using (var cn = Connection)
            {
                var sql = "select * from privilegios " +
                          "inner join privusers on privusers.codprivilegio = privilegios.codprivilegio " +
                          "where privilegios.nomeobj = @privilegio and codUser = @id";

                TryRetry.Do(() => cn.Open(), TimeSpan.FromSeconds(5));
                var usuario = cn.Query<Usuario>(sql, new { privilegio, id }).Any();
                cn.Close();

                return usuario;
            }
        }
    }
}