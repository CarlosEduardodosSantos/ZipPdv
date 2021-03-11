using System;
using System.Linq;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class NfceServicoRepository : RepositoryBase, INfceServicoRepository
    {
        public NFce GravaNfce(int vendaId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var nfceId = conn.Query<NFce>("Exec PROC_INSERT_NFCE @vendaId", new {vendaId}).FirstOrDefault();
                conn.Close();

                return nfceId;
            }
        }

        public string GeraNFce(int nfceId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var txtNfce = conn.Query<string>("Exec SP_GERA_NFE @nfceId", new { nfceId });
                conn.Close();

                var str = new System.Text.StringBuilder();
                foreach (var linha in txtNfce)
                {
                    str.AppendLine(linha);
                }
                return str.ToString();
            }
        }

        public NfceSituacao ConsultaSituacao(int nfceId, int serie, int modelo, string cnpjEmpresa)
        {
            var sql = "Select * from dbo.FUNC_RETORNA_SIT_NFE(@nfceId, @serie, @modelo, '', @cnpjEmpresa)";

            using (var conn = Connection)
            {
                conn.Open();
                var situacao = conn.Query<NfceSituacao>(sql, new { nfceId, serie, modelo, cnpjEmpresa }).FirstOrDefault();
                conn.Close();

                return situacao;
            }
        }

        public string ObterDiretorioNfce(int empresaId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var caixaEntrada = conn.Query<string>("Select DiretorioEntrada From Parametros Where Id = @empresaId", new { empresaId }).FirstOrDefault();
                conn.Close();

                return caixaEntrada;
            }
        }
    }
}