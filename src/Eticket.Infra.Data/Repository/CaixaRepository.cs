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
                      " Where cx.NroCx = @id";
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
            
            using (var conn = Connection)
            {
                conn.Open();


                var sqlFechamento = new StringBuilder();
                sqlFechamento.AppendLine("Insert Into CaixaFechamentos(CaixaId, EspecieId, Especie, Valor, Divergencia, Conferido, UsuarioConferenciaId)");
                sqlFechamento.AppendLine("Values(@CaixaId, @EspecieId, @Especie, @Valor, @Divergencia, @Conferido, @UsuarioConferenciaId)");

                decimal dev01 = 0, dev02 = 0, dev03 = 0, dev04 = 0, dev05 = 0, dev06 = 0, dev07 = 0, dev08 = 0, dev09 = 0;

                foreach (var caixaCaixaFechamento in caixa.CaixaFechamentos)
                {
                    var sqlEspecie = "Select Interno From EspeciePagamentos Where Especie = @Especie";

                    var espInterno = conn.Query<string>(sqlEspecie, new { caixaCaixaFechamento.Especie } ).FirstOrDefault();

                    if (espInterno == "ESP1")
                        dev01 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP2")
                        dev02 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP3")
                        dev03 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP4")
                        dev04 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP5")
                        dev05 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP6")
                        dev06 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP7")
                        dev07 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP8")
                        dev08 = caixaCaixaFechamento.Divergencia;
                    else if (espInterno == "ESP9")
                        dev09 = caixaCaixaFechamento.Divergencia;

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



                var sql = new StringBuilder();
                sql.AppendLine("Update Caixa_1 Set");
                sql.AppendLine("FIM_DATA = @FIM_DATA,");
                sql.AppendLine("FIM_HORA = @FIM_HORA,");
                sql.AppendLine("FIM_USUARIO = @FIM_USUARIO,");
                sql.AppendLine("Div1 = @Div1,");
                sql.AppendLine("Div2 = @Div2,");
                sql.AppendLine("Div3 = @Div3,");
                sql.AppendLine("Div4 = @Div4,");
                sql.AppendLine("Div5 = @Div5,");
                sql.AppendLine("Div6 = @Div6,");
                sql.AppendLine("Div7 = @Div7,");
                sql.AppendLine("Div8 = @Div8,");
                sql.AppendLine("Div9 = @Div9,");
                sql.AppendLine("Conferido = 0");
                sql.AppendLine("Where NROCX = @NROCX");

                var parans = new DynamicParameters();
                parans.Add("@NROCX", caixa.CaixaId);
                parans.Add("@FIM_DATA", System.DateTime.Now.Date);
                parans.Add("@FIM_HORA", System.DateTime.Now.ToString("HH:mm"));
                parans.Add("@FIM_USUARIO", caixa.UsuarioFinalId);
                parans.Add("@FIM_LOJA", caixa.Loja);
                parans.Add("@Div1", dev01);
                parans.Add("@Div2", dev02);
                parans.Add("@Div3", dev03);
                parans.Add("@Div4", dev04);
                parans.Add("@Div5", dev05);
                parans.Add("@Div6", dev06);
                parans.Add("@Div7", dev07);
                parans.Add("@Div8", dev08);
                parans.Add("@Div9", dev09);



                conn.Query(sql.ToString(), parans);


                conn.Close();
            }
        }

        public Caixa ObterCaixaAberto(int loja, int pdv)
        {
            var sql = SelecView() +
                      " Where cx.FIM_DATA IS NULL And " +
                      "cx.Loja = @loja And  pdv = @pdv";

            using (var conn = Connection)
            {
                conn.Open();
                var caixa = conn.Query<Caixa>(sql, new { loja, pdv }).FirstOrDefault();
                conn.Close();

                return caixa;
            }
        }

        private string SelecView()
        {
            return @"Select 
	                    cx.NROCX as CaixaId,
	                    cx.LOJA as Loja,
	                    cx.Pdv, 
	                    cx.USUARIO UsuarioId,
	                    cx.Data + cx.HORA as DataInicio,
	                    cx.INICIAL as ValorAbertura,
	                    cx.COD_CEDENTE as CedenteId,
	                    UF.NOME as UsuarioFinal,
	                    cx.FIM_DATA + cx.FIM_HORA as DataFinal
                    From Caixa_1 cx
                    Left Join RECEP UI On cx.USUARIO = UI.CODIGO
                    Left Join RECEP UF On cx.FIM_USUARIO = UF.CODIGO";
        }


        public Caixa ObterCaixaData(System.DateTime dtCaixa)
        {
            var sql = SelecView() +
                      " Where cx.FIM_DATA IS NOT NULL And cx.Data = @dtCaixa";

            using (var conn = Connection)
            {
                conn.Open();
                var caixa = conn.Query<Caixa>(sql, new { dtCaixa }).FirstOrDefault();
                conn.Close();

                return caixa;
            }
        }
    }
}