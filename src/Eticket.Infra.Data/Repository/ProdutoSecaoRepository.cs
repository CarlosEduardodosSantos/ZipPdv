using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class ProdutoSecaoRepository : RepositoryBase, IProdutoSecaoRepository
    {
        public ProdutoSecao GetById(int id)
        {
            var sql = $"{SelectBase} Where Codigo = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var secao = conn.Query<ProdutoSecao>(sql, new { id }).FirstOrDefault();
                conn.Close();

                return secao;
            }
        }

        public IEnumerable<ProdutoSecao> GetAll()
        {
            var sql = $"{SelectBase}";
            using (var conn = Connection)
            {
                conn.Open();
                var secoes = conn.Query<ProdutoSecao>(sql);
                conn.Close();

                return secoes;
            }
        }

        public void Adicionar(ProdutoSecao produtoSecao)
        {
            var sql = "Insert Into Secao(Codigo, DES_) Values(@Codigo, @DES_)";
            var parms = new DynamicParameters();
            parms.Add("@Codigo", MaxId());
            parms.Add("@DES_", produtoSecao.Descricao);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, parms);
                conn.Close();
            }
        }

        public void Editar(ProdutoSecao produtoSecao)
        {
            var sql = "Update Secao Set DES_ = @DES_ Where Codigo = @Codigo";
            var parms = new DynamicParameters();
            parms.Add("@Codigo", produtoSecao.SecaoId);
            parms.Add("@DES_", produtoSecao.Descricao);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, parms);
                conn.Close();
            }
        }

        public void Excluir(ProdutoSecao produtoSecao)
        {
            var sql = "Delete From Secao Where Codigo = @Codigo";
            var parms = new DynamicParameters();
            parms.Add("@Codigo", produtoSecao.SecaoId);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, parms);
                conn.Close();
            }
        }

        private int MaxId()
        {
            var sql = $"Select Max(Codigo) From Secao";
            using (var conn = Connection)
            {
                conn.Open();
                var depto = conn.Query<int>(sql).FirstOrDefault();
                conn.Close();

                return depto;
            }
        }
        string SelectBase = "select Codigo as DeptoId, DES_ as Descricao from Secao";
    }
}