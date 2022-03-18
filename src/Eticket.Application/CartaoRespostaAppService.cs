using System;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class CartaoRespostaAppService : ICartaoRespostaAppService
    {
        private readonly ICartaoRespostaRepository _cartaoRespostaRepository;

        public CartaoRespostaAppService(ICartaoRespostaRepository cartaoRespostaRepository)
        {
            _cartaoRespostaRepository = cartaoRespostaRepository;
        }

        public void Dispose()
        {
            _cartaoRespostaRepository.Dispose();
        }

        public void Adicionar(CartaoRespostaViewModel cartaoRespostaView)
        {
            var cartao = TypeAdapter.Adapt<CartaoRespostaViewModel, CartaoResposta>(cartaoRespostaView);
            _cartaoRespostaRepository.Adicionar(cartao);
        }

        public CartaoRespostaViewModel ObterPorRequisicao(int requisicao)
        {
            return TypeAdapter.Adapt<CartaoResposta, CartaoRespostaViewModel>(_cartaoRespostaRepository
                .ObterPorRequisicao(requisicao));
        }

        public CartaoRespostaViewModel ObterPorGuid(Guid cartaoRespostaGuid)
        {
            return TypeAdapter.Adapt<CartaoResposta, CartaoRespostaViewModel>(_cartaoRespostaRepository
                .ObterPorGuid(cartaoRespostaGuid));
        }

        public CartaoRespostaViewModel ObterPorVendaId(int vendaId)
        {
            return TypeAdapter.Adapt<CartaoResposta, CartaoRespostaViewModel>(_cartaoRespostaRepository
                .ObterPorVendaId(vendaId));
        }
    }
}