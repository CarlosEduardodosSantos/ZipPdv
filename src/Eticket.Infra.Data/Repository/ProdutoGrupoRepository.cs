using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class ProdutoGrupoRepository : RepositoryBase, IProdutoGrupoRepository
    {
        public ProdutoGrupo GetById(int id)
        {
            using (var cn = Connection)
            {
                var sql = "Select IdPdvGrupo as GrupoId, Descricao, Grupo_Cor as GrupoCor from pdvGrupos Where IdPdvGrupo = @id ";

                cn.Open();
                var grupo = cn.Query<ProdutoGrupo>(sql, new { id }).FirstOrDefault();
                cn.Close();

                return grupo;
            }
        }

        public IEnumerable<ProdutoGrupo> GetAll()
        {
            using (var cn = Connection)
            {
                var sql = "Select IdPdvGrupo as GrupoId, Descricao, grupo_img as Imagem, Grupo_Cor as GrupoCor from pdvGrupos Order By Sequencia";

                cn.Open();
                var grupos = cn.Query<ProdutoGrupo>(sql, new {});
                cn.Close();

                return grupos;
            }
        }
    }
}