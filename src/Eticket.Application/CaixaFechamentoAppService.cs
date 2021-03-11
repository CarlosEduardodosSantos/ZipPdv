using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class CaixaFechamentoAppService : ICaixaFechamentoAppService
    {
        private readonly ICaixaFechamentoRepository _caixaFechamentoRepository;

        public CaixaFechamentoAppService(ICaixaFechamentoRepository caixaFechamentoRepository)
        {
            _caixaFechamentoRepository = caixaFechamentoRepository;
        }

        public void Dispose()
        {
            _caixaFechamentoRepository.Dispose();
        }

        public IEnumerable<CaixaFechamentoViewModel> ObterPorCaixaId(int caixaId)
        {
            return TypeAdapter.Adapt< IEnumerable<CaixaFechamento>, IEnumerable<CaixaFechamentoViewModel>>(_caixaFechamentoRepository
                .ObterPorCaixaId(caixaId));
        }
    }
}