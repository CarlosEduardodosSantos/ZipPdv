using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICartaoRequisicaoRepository : IRepositoryBase<CartaoRequisicao>
    {
        void Adicionar(CartaoRequisicao cartaoRequisicao);
        int ObterUltimaRequisicao();
    }
}