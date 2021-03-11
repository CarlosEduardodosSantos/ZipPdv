using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CaixaRepository : RepositoryBase, ICaixaRepository
    {
        public Caixa GetById(int id)
        {
            var sql = SelecView() +
                      " Where NroCx = @id";
            using (var conn = Connection)
            {
                conn.Open();
                var caixa = conn.Query<Caixa>(sql, new {id}).FirstOrDefault();
                conn.Close();

                return caixa;
            }
        }

        public IEnumerable<Caixa> GetAll()
        {
            var sql = SelecView();
            using (var conn = Connection)
            {
                conn.Open();
                var caixas = conn.Query<Caixa>(sql);
                conn.Close();

                return caixas;
            }
        }

        public void Abrir(Caixa caixa)
        {
            var sql = new StringBuilder();
            sql.AppendLine(
                "Insert Into Caixa_1(LOJA, PDV, USUARIO, DATA, HORA, INICIAL, COD_CEDENTE, conferido)");
            sql.AppendLine(
                "Values (@LOJA, @PDV, @USUARIO, @DATA, @HORA, @INICIAL, @COD_CEDENTE, @conferido)");

            var parans = new DynamicParameters();
            parans.Add("@LOJA", caixa.Loja);
            parans.Add("@PDV", caixa.Pdv);
            parans.Add("@USUARIO", caixa.UsuarioId);
            parans.Add("@DATA", caixa.DataInicio.Date);
            parans.Add("@HORA", caixa.DataInicio.ToString("HH:mm"));
            parans.Add("@COD_CEDENTE", caixa.CedenteId);
            parans.Add("@INICIAL", caixa.ValorAbertura);
            parans.Add("@conferido", 0);
            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parans);
                conn.Close();
            }
        }

        public void Fechar(Caixa caixa)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Update Caixa_1 Set");
            sql.AppendLine("FIM_DATA = @FIM_DATA,");
            sql.AppendLine("FIM_HORA = @FIM_HORA,");
            sql.AppendLine("FIM_USUARIO = @FIM_USUARIO,");
            sql.AppendLine("Div1 = 0,");
            sql.AppendLine("Div2 = 0,");
            sql.AppendLine("Div3 = 0,");
            sql.AppendLine("Div4 = 0,");
            sql.AppendLine("Div5 = 0,");
            sql.AppendLine("Div6 = 0,");
            sql.AppendLine("Div7 = 0,");
            sql.AppendLine("Div8 = 0,");
            sql.AppendLine("Div9 = 0,");
            sql.AppendLine("Conferido = 0");
            sql.AppendLine("Where NROCX = @NROCX");

            var parans = new DynamicParameters();
            parans.Add("@NROCX", caixa.CaixaId);
            parans.Add("@FIM_DATA", System.DateTime.Now.Date);
            parans.Add("@FIM_HORA", System.DateTime.Now.ToString("HH:mm"));
            parans.Add("@FIM_USUARIO", caixa.UsuarioId);
            parans.Add("@FIM_LOJA", caixa.Loja);


            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parans);


                var sqlFechamento = new StringBuilder();
                sqlFechamento.AppendLine("Insert Into CaixaFechamentos(CaixaId, EspecieId, Especie, Valor, Divergencia, Conferido, UsuarioConferenciaId)");
                sqlFechamento.AppendLine("Values(@CaixaId, @EspecieId, @Especie, @Valor, @Divergencia, @Conferido, @UsuarioConferenciaId)");


                foreach (var caixaCaixaFechamento in caixa.CaixaFechamentos)
                {
                    var paransFechamento = new DynamicParameters();
                    paransFechamento.Add("@CaixaId", caixa.CaixaId);
                    paransFechamento.Add("@EspecieId", caixaCaixaFechamento.EspecieId);
                    paransFechamento.Add("@Especie", caixaCaixaFechamento.Especie);
                    paransFechamento.Add("@Valor", caixaCaixaFechamento.Valor);
                    paransFechamento.Add("@Divergencia", caixaCaixaFechamento.Divergencia);
                    paransFechamento.Add("@Conferido", false);
                    paransFechamento.Add("@UsuarioConferenciaId", 0);

                    conn.Query(sqlFechamento.ToString(), paransFechamento);

                }
                conn.Close();
            }
        }

        public Caixa ObterCaixaAberto(int pdv)
        {
            var sql = SelecView() +
                      " Where FIM_DATA IS NULL And " +
                      "pdv = @pdv";

            using (var conn = Connection)
            {
                conn.Open();
                var caixa = conn.Query<Caixa>(sql, new { pdv }).FirstOrDefault();
                conn.Close();

                return caixa;
            }
        }

        private string SelecView()
        {
            return @"Select 
	                    NROCX as CaixaId,
	                    LOJA as Loja,
	                    Pdv, 
	                    USUARIO UsuarioId,
	                    Data + HORA as DataInicio,
	                    INICIAL as ValorAbertura,
	                    COD_CEDENTE as CedenteId
                    From Caixa_1";
        }
    }
}