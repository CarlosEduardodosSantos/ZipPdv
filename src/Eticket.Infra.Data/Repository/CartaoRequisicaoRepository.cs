using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;

namespace Eticket.Infra.Data.Repository
{
    public class CartaoRequisicaoRepository : RepositoryBase, ICartaoRequisicaoRepository
    {
        public CartaoRequisicao GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CartaoRequisicao> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Adicionar(CartaoRequisicao cartaoRequisicao)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Insert Into CartaoRequisicoes(CartaoRequisicaoGuid, TipoOperacao, Requisicao, Vinculado, Valor, CodigoNsu,");
            sql.AppendLine("Parcelada, ParceladaLoja, QuantidadeParcela, EmpresaCnpj, DataHora)");
            sql.AppendLine("Values (@CartaoRequisicaoGuid, @TipoOperacao, @Requisicao, @Vinculado, @Valor, @CodigoNsu,");
            sql.AppendLine("@Parcelada, @ParceladaLoja, @QuantidadeParcela, @EmpresaCnpj, @DataHora)");

            var param = new DynamicParameters();
            param.Add("@CartaoRequisicaoGuid", cartaoRequisicao.CartaoRequisicaoGuid);
            param.Add("@TipoOperacao", cartaoRequisicao.TipoOperacao);
            param.Add("@Requisicao", cartaoRequisicao.Requisicao);
            param.Add("@Vinculado", cartaoRequisicao.Vinculado);
            param.Add("@Valor", cartaoRequisicao.Valor);
            param.Add("@CodigoNsu", cartaoRequisicao.CodigoNsu);
            param.Add("@Parcelada", cartaoRequisicao.Parcelada);
            param.Add("@ParceladaLoja", cartaoRequisicao.ParceladaLoja);
            param.Add("@QuantidadeParcela", cartaoRequisicao.QuantidadeParcela);
            param.Add("@EmpresaCnpj", cartaoRequisicao.EmpresaCnpj);
            param.Add("@DataHora", cartaoRequisicao.DataHora);

            using (var conn = Connection)
            {
                conn.Open();
                conn.Query(sql.ToString(), param);
                conn.Close();
            }
        }

        public int ObterUltimaRequisicao()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var ultimaRequisicao = conn.Query<int>("Select Isnull(Max(Requisicao), 0) From CartaoRequisicoes")
                    .FirstOrDefault();
                conn.Close();

                return ultimaRequisicao;

            }
        }
    }
}