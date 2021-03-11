using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class CartaoRequisicaoAppService : ICartaoRequisicaoAppService
    {
        private readonly ICartaoRequisicaoRepository _cartaoRequisicaoRepository;

        public CartaoRequisicaoAppService(ICartaoRequisicaoRepository cartaoRequisicaoRepository)
        {
            _cartaoRequisicaoRepository = cartaoRequisicaoRepository;
        }

        public void Dispose()
        {
            _cartaoRequisicaoRepository.Dispose();
        }

        public void Adicionar(CartaoRequisicaoViewModel cartaoRequisicaoView)
        {
            var cartao = TypeAdapter.Adapt<CartaoRequisicaoViewModel, CartaoRequisicao>(cartaoRequisicaoView);
            _cartaoRequisicaoRepository.Adicionar(cartao);
        }

        public int ObterUltimaRequisicao()
        {
            return _cartaoRequisicaoRepository.ObterUltimaRequisicao();
        }
    }
}