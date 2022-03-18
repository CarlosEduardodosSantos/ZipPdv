using System;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICartaoRespostaRepository : IRepositoryBase<CartaoResposta>
    {
        void Adicionar(CartaoResposta cartaoResposta);
        CartaoResposta ObterPorRequisicao(int requisicao);
        CartaoResposta ObterPorGuid(Guid cartaoRespostaGuid);
        CartaoResposta ObterPorVendaId(int vendaId);
    }
}