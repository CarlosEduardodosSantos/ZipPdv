using System;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface ICartaoRespostaAppService : IDisposable
    {
        void Adicionar(CartaoRespostaViewModel cartaoRespostaView);
        CartaoRespostaViewModel ObterPorRequisicao(int requisicao);
        CartaoRespostaViewModel ObterPorGuid(Guid cartaoRespostaGuid);
        CartaoRespostaViewModel ObterPorVendaId(int vendaId);
    }
}