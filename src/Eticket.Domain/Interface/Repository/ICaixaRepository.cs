using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICaixaRepository : IRepositoryBase<Caixa>
    {
        void Abrir(Caixa caixa);
        void Fechar(Caixa caixa);
        Caixa ObterCaixaAberto(int pdv);
    }
}