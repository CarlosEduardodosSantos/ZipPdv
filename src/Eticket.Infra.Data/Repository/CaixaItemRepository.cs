using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CaixaItemRepository : RepositoryBase, ICaixaItemRepository
    {
        public CaixaItem GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CaixaItem> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Adicionar(CaixaItem caixaItem)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into Caixa_2(CaixaItemId, NROCX, NROVENDA, VALOR, HISTORICO, Troco, COD_USUARIO, ESP1, ESP2, ESP3,");
            sql.AppendLine("ESP4, ESP5, ESP6, ESP7, ESP8, ESP9, TipoLancto, Especies)");

            sql.AppendLine("Values(@CaixaItemId, @NROCX, @NROVENDA, @VALOR, @HISTORICO, @Troco, @COD_USUARIO, @ESP1, @ESP2, @ESP3,");
            sql.AppendLine("@ESP4, @ESP5, @ESP6, @ESP7, @ESP8, @ESP9, @TipoLancto, @Especies)");

            var parms = new DynamicParameters();
            parms.Add("@CaixaItemId", caixaItem.CaixaItemId);
            parms.Add("@NROCX", caixaItem.CaixaId);
            parms.Add("@NROVENDA", caixaItem.VendaId);
            parms.Add("@VALOR", caixaItem.Valor);
            parms.Add("@HISTORICO", caixaItem.Historico);
            parms.Add("@Troco", caixaItem.Troco);
            parms.Add("@COD_USUARIO", caixaItem.UsuarioId);
            var vEsp1 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP1").Sum(t => t.Valor);
            parms.Add("@ESP1", vEsp1);
            var vEsp2 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP2").Sum(t => t.Valor);
            parms.Add("@ESP2", vEsp2);
            var vEsp3 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP3").Sum(t => t.Valor);
            parms.Add("@ESP3", vEsp3);
            var vEsp4 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP4").Sum(t => t.Valor);
            parms.Add("@ESP4", vEsp4);
            var vEsp5 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP5").Sum(t => t.Valor);
            parms.Add("@ESP5", vEsp5);
            var vEsp6 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP6").Sum(t => t.Valor);
            parms.Add("@ESP6", vEsp6);
            var vEsp7 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP7").Sum(t => t.Valor);
            parms.Add("@ESP7", vEsp7);
            var vEsp8 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP8").Sum(t => t.Valor);
            parms.Add("@ESP8", vEsp8);
            var vEsp9 = caixaItem.CaixaPagamentos.Where(t => t.Interno == "ESP9").Sum(t => t.Valor);
            parms.Add("@ESP9", vEsp9);
            parms.Add("@TipoLancto", caixaItem.TipoLancamento);

            var especieDescricao = string.Empty;

            foreach (var especieCaixa in caixaItem.CaixaPagamentos)
            {
                especieDescricao += $"{especieCaixa.Especie}: {especieCaixa.Valor} ";
            }
            parms.Add("@Especies", especieDescricao.Trim());

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parms);


                var sqlPagamento = new StringBuilder();
                sqlPagamento.AppendLine("Insert Into CaixaPagamentos(CaixaItemId, CaixaId, CartaoRespostaGuid, EspeciePagamentoId, Especie, Valor, Interno)");
                sqlPagamento.AppendLine("Values (@CaixaItemId, @CaixaId,  @CartaoRespostaGuid, @EspeciePagamentoId, @Especie, @Valor, @Interno)");

                foreach (var caixaItemCaixaPagamento in caixaItem.CaixaPagamentos)
                {
                    var parmspagamento = new DynamicParameters();
                    parmspagamento.Add("@CaixaItemId", caixaItem.CaixaItemId);
                    parmspagamento.Add("@CaixaId", caixaItem.CaixaId);
                    parmspagamento.Add("@CartaoRespostaGuid", caixaItemCaixaPagamento.CartaoRespostaGuid);
                    parmspagamento.Add("@EspeciePagamentoId", caixaItemCaixaPagamento.EspeciePagamentoId);
                    parmspagamento.Add("@Especie", caixaItemCaixaPagamento.Especie);
                    parmspagamento.Add("@Valor", caixaItemCaixaPagamento.Valor);
                    parmspagamento.Add("@Interno", caixaItemCaixaPagamento.Interno);

                    conn.Query(sqlPagamento.ToString(), parmspagamento);
                }

                conn.Close();
            }
        }

        public IEnumerable<CaixaItem> ObterPorCaixaId(int caixaId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Select");
            sql.AppendLine("CaixaItem.CaixaItemId,");
            sql.AppendLine("CaixaItem.NROCX as CaixaId,");
            sql.AppendLine("CaixaItem.NROVENDA as VendaId,");
            sql.AppendLine("CaixaItem.VALOR as Valor,");
            sql.AppendLine("CaixaItem.HISTORICO as Historico,");
            sql.AppendLine("CaixaItem.Troco as Troco,");
            sql.AppendLine("CaixaItem.TipoLancto as TipoLancamento,");
            sql.AppendLine("CaixaItem.COD_USUARIO as UsuarioId,");
            sql.AppendLine("CaixaPagamentos.*");
            sql.AppendLine("From Caixa_2 CaixaItem");
            sql.AppendLine("Inner Join CaixaPagamentos On CaixaItem.CaixaItemId = CaixaPagamentos.CaixaItemId");
            sql.AppendLine("Where CaixaItem.NROCX = @caixaId");

            using (var conn = Connection)
            {
                conn.Open();

                var identityMap = new Dictionary<Guid, CaixaItem>();

                var caixaItem = conn.Query<CaixaItem, CaixaPagamento, CaixaItem>(sql.ToString(),
                    (ci, ch) =>
                    {
                        CaixaItem master;
                        if (!identityMap.TryGetValue(ci.CaixaItemId, out master))
                        {
                            identityMap[ci.CaixaItemId] = master = ci;
                        }
                        var list = (List<CaixaPagamento>)master.CaixaPagamentos;
                        if (list == null)
                        {
                            master.CaixaPagamentos = list = new List<CaixaPagamento>();
                        }
                        list.Add(ch);

                        return master;
                    }, new { caixaId }, splitOn: "CaixaItemId, CaixaPagamentoId").Distinct();

                conn.Close();

                return caixaItem;
            }
        }
    }
}