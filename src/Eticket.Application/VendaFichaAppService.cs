using System;
using System.Collections.Generic;
using System.Linq;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class VendaFichaAppService : IVendaFichaAppService
    {
        private readonly IVendaFichaRepository _vendaFichaRepository;

        public VendaFichaAppService(IVendaFichaRepository vendaFichaRepository)
        {
            _vendaFichaRepository = vendaFichaRepository;
        }

        public void Dispose()
        {
            _vendaFichaRepository.Dispose();
        }

        public void Add(IEnumerable<VendaFichaViewModel> vendaFichaViews)
        {
            var vendaFichaViewModels = vendaFichaViews as VendaFichaViewModel[] ?? vendaFichaViews.ToArray();
            foreach (var vendaFichaViewModel in vendaFichaViewModels)
            {
                var fichaItem = TypeAdapter.Adapt<VendaFichaViewModel, VendaFicha>(vendaFichaViewModel);
                _vendaFichaRepository.Add(fichaItem);
            }
        }

        public void Remover(VendaFichaViewModel vendaFichaView)
        {
            var fichaItem = TypeAdapter.Adapt<VendaFichaViewModel, VendaFicha>(vendaFichaView);
            _vendaFichaRepository.Remover(fichaItem);
        }

        public IEnumerable<VendaFichaViewModel> ObterPorFicha(int[] ficha)
        {
            return
                TypeAdapter.Adapt<IEnumerable<VendaFicha>, IEnumerable<VendaFichaViewModel>>(
                    _vendaFichaRepository.ObterPorFicha(ficha));
        }

        public int ObterUltimaSequencia(string ficha)
        {
            return _vendaFichaRepository.ObterUltimaSequencia(ficha);
        }

        public void ImprimeFichaGr(string ficha)
        {
            _vendaFichaRepository.ImprimeFichaGr(ficha);
        }

        public void FinalizaFicha(string fichaId)
        {
            _vendaFichaRepository.FinalizaFicha(fichaId);
        }

        public bool FicheExiste(int fichaId)
        {
            return _vendaFichaRepository.FicheExiste(fichaId);
        }

        public VendaFichaViewModel ObterFichaByGuid(string fichaGuid)
        {
            return
                TypeAdapter.Adapt<VendaFicha, VendaFichaViewModel>(
                    _vendaFichaRepository.ObterFichaByGuid(fichaGuid));
        }
    }
}