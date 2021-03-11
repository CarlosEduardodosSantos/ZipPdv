using System;
using System.Collections.Generic;
using Eticket.Application.Interface;
using Eticket.Application.ViewModels;
using Eticket.Domain.Entity;
using Eticket.Domain.Interface.Repository;
using FastMapper;

namespace Eticket.Application
{
    public class FabricaRelatorioAppService : IFabricaRelatorioAppService
    {
        private readonly IFabricaRelatorioRepository _fabricaRelatorioRepository;

        public FabricaRelatorioAppService(IFabricaRelatorioRepository fabricaRelatorioRepository)
        {
            _fabricaRelatorioRepository = fabricaRelatorioRepository;
        }

        public void Dispose()
        {
            _fabricaRelatorioRepository.Dispose();
        }

        public void Adcionar(FabricaRelatorioViewModel fabricaRelatorioViewModel)
        {
            var relatorio = TypeAdapter.Adapt<FabricaRelatorioViewModel, FabricaRelatorio>(fabricaRelatorioViewModel);
            _fabricaRelatorioRepository.Adcionar(relatorio);
        }

        public void Editar(FabricaRelatorioViewModel fabricaRelatorioViewModel)
        {
            var relatorio = TypeAdapter.Adapt<FabricaRelatorioViewModel, FabricaRelatorio>(fabricaRelatorioViewModel);
            _fabricaRelatorioRepository.Editar(relatorio);
        }

        public void Remover(FabricaRelatorioViewModel fabricaRelatorioViewModel)
        {
            var relatorio = TypeAdapter.Adapt<FabricaRelatorioViewModel, FabricaRelatorio>(fabricaRelatorioViewModel);
            _fabricaRelatorioRepository.Remover(relatorio);
        }

        public FabricaRelatorioViewModel ObterPorId(Guid id)
        {
            return TypeAdapter.Adapt<FabricaRelatorio, FabricaRelatorioViewModel>(_fabricaRelatorioRepository.GetByGuid(id));
        }

        public IEnumerable<FabricaRelatorioViewModel> ObterTodos()
        {
            return
                TypeAdapter.Adapt<IEnumerable<FabricaRelatorio>, IEnumerable<FabricaRelatorioViewModel>>(
                    _fabricaRelatorioRepository.GetAll());
        }

        public IEnumerable<FabricaRelatorioViewModel> ObterPorPesquisa(string pesquisa)
        {
            return
                TypeAdapter.Adapt<IEnumerable<FabricaRelatorio>, IEnumerable<FabricaRelatorioViewModel>>(
                    _fabricaRelatorioRepository.ObterPorPesquisa(pesquisa));
        }
    }
}