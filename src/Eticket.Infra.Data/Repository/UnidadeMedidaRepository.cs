using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface;

namespace Eticket.Infra.Data.Repository
{
    public class UnidadeMedidaRepository : RepositoryBase, IUnidadeMedidaRepository
    {
        public UnidadeMedida GetById(int id)
        {
            var sql = "select COD_UNI as UnidadeId, CODIGO as Unidade, DESCRICAO as Descricao from Tab_uni Where Cod_uni = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var unidade = conn.Query<UnidadeMedida>(sql, new {id}).FirstOrDefault();
                conn.Close();

                return unidade;
            }
        }

        public IEnumerable<UnidadeMedida> GetAll()
        {
            var sql = "select COD_UNI as UnidadeId, CODIGO as Unidade, DESCRICAO as Descricao from Tab_uni";
            using (var conn = Connection)
            {
                conn.Open();
                var unidades = conn.Query<UnidadeMedida>(sql);
                conn.Close();

                return unidades;
            }
        }

        public void Adicionar(UnidadeMedida unidade)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into Tab_uni(CODIGO, DESCRICAO, FRACIONADO)");
            sql.AppendLine("Values(@CODIGO, @DESCRICAO, @FRACIONADO)");
            var parm = new DynamicParameters();
            parm.Add("@CODIGO", unidade.Unidade);
            parm.Add("@DESCRICAO", unidade.Descricao);
            parm.Add("@FRACIONADO", unidade.Fracionado);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parm);
                conn.Close();
            }
        }

        public void Alterar(UnidadeMedida unidade)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Update  Tab_uni Set (CODIGO, DESCRICAO, FRACIONADO)");
            sql.AppendLine("CODIGO = @CODIGO");
            sql.AppendLine("DESCRICAO = @DESCRICAO");
            sql.AppendLine("FRACIONADO = @FRACIONADO");
            sql.AppendLine("Where COD_UNI = @COD_UNI");
            var parm = new DynamicParameters();
            parm.Add("@COD_UNI", unidade.Unidade);
            parm.Add("@CODIGO", unidade.Unidade);
            parm.Add("@DESCRICAO", unidade.Descricao);
            parm.Add("@FRACIONADO", unidade.Fracionado);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parm);
                conn.Close();
            }
        }

        public void Remover(UnidadeMedida unidade)
        {
            var sql = "Delete From Tab_uni Where COD_UNI = @unidade";

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, new {unidade});
                conn.Close();
            }
        }
    }
}