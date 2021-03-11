using System;
using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface;
using FastMapper;

namespace Eticket.Application
{
    public class VendaAppService : IVendaAppService
    {
        private readonly IVendaRepository _vendaRepository;

        public VendaAppService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public void Dispose()
        {
            _vendaRepository.Dispose();
        }

        public void Adicionar(VendaViewModel vendaView)
        {
            var venda = TypeAdapter.Adapt<VendaViewModel, Venda>(vendaView);
            _vendaRepository.Adicionar(venda);
        }

        public void Cancelar(VendaViewModel vendaView)
        {
            var venda = TypeAdapter.Adapt<VendaViewModel, Venda>(vendaView);
            _vendaRepository.Cancelar(venda);
        }

        public VendaViewModel ObterPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<VendaViewModel> ObterEntregaPendentes()
        {
            return  TypeAdapter.Adapt<IEnumerable<Venda>, IEnumerable<VendaViewModel>>(_vendaRepository.ObterEntregaPendentes());
        }

        public IEnumerable<VendaViewModel> ObterEntregaAguardandoRetorno()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<VendaViewModel> ObterPorData(DateTime dataInicio, DateTime dataFinal)
        {
            return TypeAdapter.Adapt<IEnumerable<Venda>, IEnumerable<VendaViewModel>>(_vendaRepository.ObterData(dataInicio, dataFinal));
        }

        public IEnumerable<VendaViewModel> ObterPendenteSat(DateTime dataInicio, DateTime dataFinal, int pdv)
        {
            return TypeAdapter.Adapt<IEnumerable<Venda>, IEnumerable<VendaViewModel>>(_vendaRepository.ObterPendenteSat(dataInicio, dataFinal, pdv));
        }

        public IEnumerable<VendaViewModel> ObterNroSat(string nroSat)
        {
            return TypeAdapter.Adapt<IEnumerable<Venda>, IEnumerable<VendaViewModel>>(_vendaRepository.ObterNroSat(nroSat));
        }

        public int ObterVendaId()
        {
            return _vendaRepository.VendaId();
        }

        public bool GeraImpressaoFechamento(int vendaId, int tipoOperacao)
        {
            return _vendaRepository.GeraImpressaoFechamento(vendaId, tipoOperacao);
        }
    }
}