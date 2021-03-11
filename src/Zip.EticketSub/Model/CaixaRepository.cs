using System;
using System.Linq;
using System.Text;
using Dapper;
using Zip.EticketSub.Repository;

namespace Zip.EticketSub.Model
{
    public class CaixaRepository : BaseRepository
    {
        public int ObterCaixaId()
        {
            var sql = new StringBuilder().AppendLine("select * from caixa_1 where loja = @loja And fim_data is null and PDV = @pdv");
            var parns = new DynamicParameters();
            parns.Add("@loja", Program.Loja);
            parns.Add("@pdv", Program.Pdv);

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    var result = conn.Query<int>(sql.ToString(), parns).FirstOrDefault();
                    conn.Close();

                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
    }
}