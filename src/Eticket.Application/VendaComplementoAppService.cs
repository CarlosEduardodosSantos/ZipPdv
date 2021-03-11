using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class VendaComplementoAppService : IVendaComplementoAppService
    {
        private readonly IVendaComplementoRepository _complementoRepository;

        public VendaComplementoAppService(IVendaComplementoRepository complementoRepository)
        {
            _complementoRepository = complementoRepository;
        }

        public void Dispose()
        {
            _complementoRepository.Dispose();
        }

        public void Add(VendaComplementoViewModel vendaComplementoView)
        {
            var vendaComplemento = TypeAdapter.Adapt<VendaComplementoViewModel, VendaComplemento>(vendaComplementoView);
            _complementoRepository.Add(vendaComplemento);
        }

        public void Remove(VendaComplementoViewModel vendaComplementoView)
        {
            var vendaComplemento = TypeAdapter.Adapt<VendaComplementoViewModel, VendaComplemento>(vendaComplementoView);
            _complementoRepository.Remove(vendaComplemento);
        }

        public VendaComplementoViewModel GetById(int id)
        {
            return TypeAdapter.Adapt<VendaComplemento, VendaComplementoViewModel>(_complementoRepository.GetById(id));
        }

        public IEnumerable<VendaComplementoViewModel> ObterPorFicha(string ficha)
        {
            return
                TypeAdapter.Adapt<IEnumerable<VendaComplemento>, IEnumerable<VendaComplementoViewModel>>(
                    _complementoRepository.ObterPorFicha(ficha));
        }

        public IEnumerable<VendaComplementoViewModel> ObterPorPendenciaId(int pendenciaId)
        {
            return
                TypeAdapter.Adapt<IEnumerable<VendaComplemento>, IEnumerable<VendaComplementoViewModel>>(
                    _complementoRepository.ObterPorPendenciaId(pendenciaId));
        }

        public IEnumerable<VendaComplementoViewModel> ObterPorMesaId(int mesaId)
        {
            return
                TypeAdapter.Adapt<IEnumerable<VendaComplemento>, IEnumerable<VendaComplementoViewModel>>(
                    _complementoRepository.ObterPorMesaId(mesaId));
        }

        public IEnumerable<VendaComplementoViewModel> ObterPorVendaId(int vendaId)
        {
            return
                TypeAdapter.Adapt<IEnumerable<VendaComplemento>, IEnumerable<VendaComplementoViewModel>>(
                    _complementoRepository.ObterPorVendaId(vendaId));
        }
    }
}