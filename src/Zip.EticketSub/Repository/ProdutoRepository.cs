using System.Linq;
using Dapper;
using Zip.EticketSub.Model;

namespace Zip.EticketSub.Repository
{
    public class ProdutoRepository : BaseRepository
    {
        public bool VerificaRelacao(string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = conn.Query<Produto>("Select * From Prod Where Codigo = @codigo", new { codigo = id }).Any();
                conn.Close();

                return result;
            }
        }

        public Produto ObterPorId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = conn
                    .Query<Produto>("Select CODIGO as produtoId, DES_ as nome, VLCusto as ValorCusto From Prod Where Codigo = @codigo",
                        new { codigo = id }).FirstOrDefault();
                conn.Close();

                return result;
            }
        }
    }
}