using System.Collections.Generic;
using Eticket.Domain.Entity;

namespace Eticket.Domain.Interface.Repository
{
    public interface ICaixaFechamentoRepository : IRepositoryBase<CaixaFechamento>
    {
        IEnumerable<CaixaFechamento> ObterPorCaixaId(int caixaId);
    }
}