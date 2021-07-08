using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class TributacaoFiscalRepository : RepositoryBase, ITributacaoFiscalRepository
    {
        public ProdutoTributacao GetById(int id)
        {
            var sql = "Select * From TributacaoFiscal Where TributacaoId = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var tributacao = conn.Query<ProdutoTributacao>(sql, new {id}).FirstOrDefault();
                conn.Close();
                return tributacao;
            }
        }

        public IEnumerable<ProdutoTributacao> GetAll()
        {
            var sql = "Select * From TributacaoFiscal";
            using (var conn = Connection)
            {
                conn.Open();
                var tributacoes = conn.Query<ProdutoTributacao>(sql);
                conn.Close();
                return tributacoes;
            }
        }

        public void Adicionar(ProdutoTributacao tributacaoFiscal)
        {
            throw new System.NotImplementedException();
        }

        public void Editar(ProdutoTributacao tributacaoFiscal)
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(ProdutoTributacao tributacaoFiscal)
        {
            throw new System.NotImplementedException();
        }
    }
}