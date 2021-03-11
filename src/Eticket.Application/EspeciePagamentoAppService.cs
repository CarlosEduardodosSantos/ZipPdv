using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class EspeciePagamentoAppService : IEspeciePagamentoAppService
    {
        private readonly IEspeciePagamentoRepository _especiePagamentoRepository;

        public EspeciePagamentoAppService(IEspeciePagamentoRepository especiePagamentoRepository)
        {
            _especiePagamentoRepository = especiePagamentoRepository;
        }

        public EspeciePagamentoViewModel ObterPorId(int id)
        {
            return TypeAdapter.Adapt<EspeciePagamento, EspeciePagamentoViewModel>(_especiePagamentoRepository.GetById(id));
        }

        public IEnumerable<EspeciePagamentoViewModel> ObterTodos()
        {
            return TypeAdapter.Adapt<IEnumerable<EspeciePagamento>, IEnumerable<EspeciePagamentoViewModel>>(_especiePagamentoRepository.GetAll());
        }
        public void Dispose()
        {
            _especiePagamentoRepository.Dispose();
        }
    }
}