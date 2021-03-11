using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

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

                cn.Open();
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

                cn.Open();
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

                cn.Open();
                var usuario = cn.Query<Usuario>(sql, new { senha }).FirstOrDefault();
                cn.Close();

                return usuario;
            }
        }
    }
}