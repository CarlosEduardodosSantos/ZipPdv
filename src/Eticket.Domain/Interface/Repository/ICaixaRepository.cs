using Eticket.Domain.Entity;
using System;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICaixaRepository : IRepositoryBase<Caixa>
    {
        void Abrir(Caixa caixa);
        void Fechar(Caixa caixa);
        Caixa ObterCaixaAberto(int loja, int pdv);
        Caixa ObterCaixaData(DateTime dtCaixa);
        Caixa ObterUltimoCaixaFechado(int loja, int pdv);
    }
}