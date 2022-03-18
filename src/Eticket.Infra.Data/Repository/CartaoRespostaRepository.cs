using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CartaoRespostaRepository : RepositoryBase, ICartaoRespostaRepository
    {
        public CartaoResposta GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartaoResposta> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Adicionar(CartaoResposta cartaoResposta)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into CartaoRespostas(CartaoRespostaGuid, TipoOperacao, Autorizado,  CodigoAutorizacao, DataHora, Requisicao, Vinculado,");
            sql.AppendLine("Bandeira, NomeRede, CnpjRede, Valor, CodigoNsu, NumeroCartao, ValorRestante, LoteAutorizacao,");
            sql.AppendLine("DataTransacao, DataComprovante, Menssagem, Comprovante, QuantidadeParcela, Operadora, TipoTransacao, RequisicaoCancelamento)");
            sql.AppendLine("Values(@CartaoRespostaGuid, @TipoOperacao, @Autorizado, @CodigoAutorizacao, @DataHora, @Requisicao, @Vinculado,");
            sql.AppendLine("@Bandeira, @NomeRede, @NomeRede, @Valor, @CodigoNsu, @NumeroCartao, @ValorRestante, @LoteAutorizacao,");
            sql.AppendLine("@DataTransacao, @DataComprovante, @Menssagem, @Comprovante, @QuantidadeParcela, @Operadora, @TipoTransacao, @RequisicaoCancelamento)");

            var parm = new DynamicParameters();
            parm.Add("@CartaoRespostaGuid", cartaoResposta.CartaoRespostaGuid);
            parm.Add("@TipoOperacao", cartaoResposta.TipoOperacao);
            parm.Add("@Autorizado", cartaoResposta.Autorizado);
            parm.Add("@CodigoAutorizacao", cartaoResposta.CodigoAutorizacao);
            parm.Add("@DataHora", cartaoResposta.DataHora);
            parm.Add("@Requisicao", cartaoResposta.Requisicao);
            parm.Add("@Vinculado", cartaoResposta.Vinculado);
            parm.Add("@Bandeira", cartaoResposta.Bandeira);
            parm.Add("@NomeRede", cartaoResposta.NomeRede);
            parm.Add("@CnpjRede", cartaoResposta.CnpjRede);
            parm.Add("@Valor", cartaoResposta.Valor);
            parm.Add("@CodigoNsu", cartaoResposta.CodigoNsu);
            parm.Add("@NumeroCartao", cartaoResposta.NumeroCartao);
            parm.Add("@ValorRestante", cartaoResposta.ValorRestante);
            parm.Add("@LoteAutorizacao", cartaoResposta.LoteAutorizacao);
            parm.Add("@DataTransacao", cartaoResposta.DataTransacao);
            parm.Add("@DataComprovante", cartaoResposta.DataComprovante);
            parm.Add("@Menssagem", cartaoResposta.Menssagem);
            parm.Add("@Comprovante", cartaoResposta.Comprovante);
            parm.Add("@QuantidadeParcela", cartaoResposta.QuantidadeParcela);
            parm.Add("@Operadora", cartaoResposta.Operadora);
            parm.Add("@TipoTransacao", cartaoResposta.TipoTransacao);
            parm.Add("@RequisicaoCancelamento", cartaoResposta.RequisicaoCancelamento);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), parm);
                conn.Close();
            }
        }

        public CartaoResposta ObterPorRequisicao(int requisicao)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var cartao = conn.Query<CartaoResposta>("Select * From CartaoRespostas Where Requisicao = @requisicao",
                    new { requisicao }).FirstOrDefault();
                conn.Close();

                return cartao;
            }
        }

        public CartaoResposta ObterPorGuid(Guid cartaoRespostaGuid)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var cartao = conn.Query<CartaoResposta>("Select * From CartaoRespostas Where CartaoRespostaGuid = @cartaoRespostaGuid",
                    new { cartaoRespostaGuid }).FirstOrDefault();
                conn.Close();

                return cartao;
            }
        }

        public CartaoResposta ObterPorVendaId(int vendaId)
        {
            using (var conn = Connection)
            {
                var sql = new StringBuilder();
                sql.AppendLine("Select * from CaixaPagamentos");
                sql.AppendLine("Inner Join Caixa_2 On CaixaPagamentos.CaixaItemId = Caixa_2.CaixaItemID");
                sql.AppendLine("Left Join CartaoRespostas On CaixaPagamentos.CartaoRespostaGuid = CartaoRespostas.CartaoRespostaGuid");
                sql.AppendLine("Where NroVenda = @vendaId ");
                conn.Open();
                var cartao = conn.Query<CartaoResposta>(sql.ToString(), new { vendaId }).FirstOrDefault();
                conn.Close();

                return cartao;
            }
        }
    }
}