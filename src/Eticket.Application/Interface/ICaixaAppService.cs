using System;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;

namespace Eticket.Application.Interface
{
    public interface ICaixaAppService : IDisposable
    {
        void Abrir(CaixaViewModel caixaView);
        void Fechar(CaixaViewModel caixaView);
        CaixaViewModel ObterCaixaAberto(int loja, int pdv);
        CaixaViewModel ObterCaixaId(int caixaId);
        CaixaViewModel ObterCaixaData(DateTime dtCaixa);
        CaixaViewModel ObterUltimoCaixaFechado(int loja, int pdv);
    }
}