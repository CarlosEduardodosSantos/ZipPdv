using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class ProdutoDeptoRepository : RepositoryBase, IProdutoDeptoRepository
    {
        public ProdutoDepto GetById(int id)
        {
            var sql = $"{SelectBase} Where Codigo = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var depto = conn.Query<ProdutoDepto>(sql, new {id}).FirstOrDefault();
                conn.Close();

                return depto;
            }
        }

        public IEnumerable<ProdutoDepto> GetAll()
        {
            var sql = $"{SelectBase}";
            using (var conn = Connection)
            {
                conn.Open();
                var deptos = conn.Query<ProdutoDepto>(sql);
                conn.Close();

                return deptos;
            }
        }

        public void Adicionar(ProdutoDepto produtoSecao)
        {
            var sql = "Insert Into Depto(Codigo, DES_) Values(@Codigo, @DES_)";
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

        public void Editar(ProdutoDepto produtoSecao)
        {
            var sql = "Update Depto Set DES_ = @DES_ Where Codigo = @Codigo";
            var parms = new DynamicParameters();
            parms.Add("@Codigo", produtoSecao.DeptoId);
            parms.Add("@DES_", produtoSecao.Descricao);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, parms);
                conn.Close();
            }
        }

        public void Excluir(ProdutoDepto produtoSecao)
        {
            var sql = "Delete From Depto Where Codigo = @Codigo";
            var parms = new DynamicParameters();
            parms.Add("@Codigo", produtoSecao.DeptoId);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql, parms);
                conn.Close();
            }
        }

        private int MaxId()
        {
            var sql = $"Select Max(Codigo) From Depto";
            using (var conn = Connection)
            {
                conn.Open();
                var depto = conn.Query<int>(sql).FirstOrDefault();
                conn.Close();

                return depto;
            }
        }

        string SelectBase = "select Codigo as DeptoId, DES_ as Descricao from Depto";
    }
}