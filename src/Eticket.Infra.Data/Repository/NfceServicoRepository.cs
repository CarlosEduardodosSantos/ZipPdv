using System;
using System.Collections.Generic;
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

        public IEnumerable<NFce> NfceNãoEnviadas(DateTime datahora)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var timeout = conn.ConnectionTimeout;

                var caixaEntrada = conn.Query<NFce>(@"SELECT NF4.NF1_ID AS NFCEID, 
                                        NF4.NF1_NRO AS NUMERONFCE, 
                                        NF4.NF1_SERIE AS SERIE, 
                                        NF4.NF1_MOD AS MODELO,
                                        NF4.NF4_VENDA AS VENDAID
                                FROM NF4
                                LEFT JOIN NF1 ON NF4.NF1_ID = NF1.NF1_ID
                                WHERE NF1_DTEMI >= @datahora
                                AND NOT EXISTS (SELECT 1 FROM NOTASFISCAIS
				                                Where 
					                                CAST(SUBSTRING(NOTASFISCAIS.CHAVENOTA,29,9) AS INT) = NF4.NF1_NRO   AND
					                                CAST(SUBSTRING(NOTASFISCAIS.CHAVENOTA,26,3) AS INT)	= NF4.NF1_SERIE AND
					                                CAST(SUBSTRING(NOTASFISCAIS.CHAVENOTA,24,2) AS INT) = NF4.NF1_MOD   AND
					                                --NOTASFISCAIS.CNPJ = SIEMP.SIEMP_CNPJ                                AND 
                                                    --NOTASFISCAIS.CodigoSituacao = 15                                    AND
                                                    NOTASFISCAIS.DataSituacao >= @datahora)", new { datahora });
                conn.Close();

                return caixaEntrada;
            }
        }
    }
}