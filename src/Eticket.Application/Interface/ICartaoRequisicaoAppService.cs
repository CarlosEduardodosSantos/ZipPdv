using System;
using Eticket.Application.ViewModels;

namespace Eticket.Application.Interface
{
    public interface ICartaoRequisicaoAppService : IDisposable
    {
        void Adicionar(CartaoRequisicaoViewModel cartaoRequisicaoView);
        int ObterUltimaRequisicao();
    }
}