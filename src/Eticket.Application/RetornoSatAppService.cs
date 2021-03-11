using System;
using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class RetornoSatAppService : IRetornoSatAppService
    {
        private readonly IRetornoSatRepository _retornoSatRepository;

        public RetornoSatAppService(IRetornoSatRepository retornoSatRepository)
        {
            _retornoSatRepository = retornoSatRepository;
        }

        public void Dispose()
        {
            _retornoSatRepository.Dispose();
        }

        public void Adicionar(RetornoSatViewModel retornoSatView)
        {
            var retornoSat = TypeAdapter.Adapt<RetornoSatViewModel, RetornoSat>(retornoSatView);
            _retornoSatRepository.Adicionar(retornoSat);
        }

        public RetornoSatViewModel ObterPorVendaId(string vendaId)
        {
            return TypeAdapter.Adapt<RetornoSat, RetornoSatViewModel>(_retornoSatRepository.ObterPorVendaId(vendaId));
        }

        public IEnumerable<RetornoSatViewModel> ObterPorData(DateTime dataInicio, DateTime dataFinal)
        {
            return TypeAdapter.Adapt<IEnumerable<RetornoSat>, IEnumerable<RetornoSatViewModel>>(_retornoSatRepository.ObterPorData(dataInicio, dataFinal));
        }
    }
}