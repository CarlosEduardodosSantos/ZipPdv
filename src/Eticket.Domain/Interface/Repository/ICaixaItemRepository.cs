using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICaixaItemRepository : IRepositoryBase<CaixaItem>
    {
        void Adicionar(CaixaItem caixaItem);
        void Remover(string caixaItemId);
        IEnumerable<CaixaItem> ObterPorCaixaId(int caixaId);
        IEnumerable<CaixaPagamento> ObterPagamentoPorVendaId(int vendaId);
    }
}